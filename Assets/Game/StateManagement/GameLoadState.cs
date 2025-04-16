using System.Threading.Tasks;
using Asteroids.Game.UI;
using Game.AssetManagement;
using Game.Configurations;
using Game.Services;
using Game.UI;
using JetBrains.Annotations;
using Zenject;

namespace Game.StateManagement
{
    [UsedImplicitly]
    public class GameLoadState : BaseGameState
    {
        private const string GameConfigAddress = "Assets/Config/GameConfig.asset";

        [Inject] private IAssetProvider _assetProvider;
        [Inject] private GameStateManager gameStateManager;
        [Inject] private IConfigCollectionService _configCollection;
        
        public override void Enter()
        {
            base.Enter();

            _ = ProcessStateAsync();
        }

        private async Task ProcessStateAsync()
        {
            await LoadGameConfig();
            
            var mainMenu = MenuManager.ShowMenu<MainMenuView>();
            MenuManager.HideMenu<GameplayView>();
            mainMenu.ToggleStartButton(false);
            
            gameStateManager.SetState<GameReadyState>();
        }
        
        private async Task LoadGameConfig()
        {
           GameConfig config = await _assetProvider.LoadAssetAsync<GameConfig>(GameConfigAddress);
           _configCollection.SetGameConfig(config);
        }
    }
}