using System.Collections.Generic;
using System.Threading.Tasks;
using Game.AssetManagement;
using Game.Configurations;
using JetBrains.Annotations;
using UnityEngine.ResourceManagement.ResourceLocations;
using Zenject;

namespace Game.StateManagement
{
    [UsedImplicitly]
    public class GameLoadState : BaseGameState
    {
        private const string GameConfigAddress = "Assets/Config/GameConfig.asset";
        private readonly List<string> _uiLabel = new() { "ui" };

        private IAssetProvider _assetProvider;
        private GameStateManager _gameStateManager;

        [Inject]
        private void InitState(IAssetProvider assetProvider,
            GameStateManager gameStateManager)
        {
            _assetProvider = assetProvider;
            _gameStateManager = gameStateManager;
        }

        public override void Enter()
        {
            base.Enter();

            _ = ProcessStateAsync();
        }

        private async Task ProcessStateAsync()
        {
            await LoadGameConfig();

            IList<IResourceLocation> uiResourceLocations = await _assetProvider.LoadAssetLabels(_uiLabel);
            await UiManager.LoadMenus(uiResourceLocations);

            _gameStateManager.SetState<GameReadyState>();
        }

        private async Task LoadGameConfig()
        {
            GameConfig config = await _assetProvider.LoadAssetAsync<GameConfig>(GameConfigAddress);
            ConfigCollection.SetGameConfig(config);
        }
    }
}