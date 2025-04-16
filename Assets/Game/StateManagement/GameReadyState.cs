using Game.Services;
using Game.UI.Gameplay;
using Game.UI.MainMenu;
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
            MainMenuView mainMenu = MenuManager.ShowMenu<MainMenuView>();
            MenuManager.HideMenu<GameplayView>();
            mainMenu.ToggleStartButton(true);
        }

        public override void Exit()
        {
            _spawnService.Initialize();
        }
    }
}