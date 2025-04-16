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

        [Inject] private IAssetProvider _assetProvider;
        [Inject] private IConfigCollectionService _configCollection;
        [Inject] private GameStateManager gameStateManager;

        public override void Enter()
        {
            base.Enter();

            _ = ProcessStateAsync();
        }

        private async Task ProcessStateAsync()
        {
            await LoadGameConfig();

            IList<IResourceLocation> uiResourceLocations = await _assetProvider.LoadAssetLabels(_uiLabel);
            await MenuManager.LoadMenus(uiResourceLocations);

            gameStateManager.SetState<GameReadyState>();
        }

        private async Task LoadGameConfig()
        {
            GameConfig config = await _assetProvider.LoadAssetAsync<GameConfig>(GameConfigAddress);
            _configCollection.SetGameConfig(config);
        }
    }
}