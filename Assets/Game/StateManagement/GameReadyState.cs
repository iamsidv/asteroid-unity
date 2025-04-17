using Game.Engine.Core;
using Game.UI.Gameplay;
using Game.UI.MainMenu;
using JetBrains.Annotations;
using Zenject;

namespace Game.StateManagement
{
    [UsedImplicitly]
    public class GameReadyState : BaseGameState
    {
        [Inject] private GameEntitySpawnController _spawnController;

        public override void Enter()
        {
            _spawnController.InstantiatePlayerShip();

            MainMenuView mainMenu = UiManager.ShowMenu<MainMenuView>();
            UiManager.HideMenu<GameplayView>();
            mainMenu.ToggleStartButton(true);
        }

        public override void Exit()
        {
            UiManager.HideMenu<MainMenuView>();
        }
    }
}