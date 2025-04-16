using Asteroids.Game.UI;
using Game.Engine;
using Game.Services;
using Game.UI;
using JetBrains.Annotations;
using Zenject;

namespace Game.StateManagement
{
    [UsedImplicitly]
    public class GameOverState : BaseGameState
    {
        [Inject] private GameContainer _gameContainer;
        
        public override void Enter()
        {
            base.Enter();

            _gameContainer.CleanupGameEntities();
            
            MenuManager.HideMenu<MainMenuView>();
            var menu = MenuManager.GetMenu<GameplayView>();
            menu.DisplayScore(PlayerProfileService.GetScore());
            menu.SetTitle("Gameover");
            menu.Clear();

            menu.DelayedStartNewGame();
        }
    }
}