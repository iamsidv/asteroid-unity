using Asteroids.Game.UI;
using Game.Services;
using Game.UI;
using JetBrains.Annotations;
using Zenject;

namespace Game.StateManagement
{
    [UsedImplicitly]
    public class GameReadyState : BaseGameState
    {
        [Inject] private GameEntitySpawnService _spawnService;
        
        public override void Enter()
        {
            base.Enter();

            _spawnService.Initialize();
            
            var mainMenu = MenuManager.ShowMenu<MainMenuView>();
            MenuManager.HideMenu<GameplayView>();
            mainMenu.ToggleStartButton(true);
        }
    }
}