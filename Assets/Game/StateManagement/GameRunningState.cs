using Game.Engine;
using Game.UI.Gameplay;
using JetBrains.Annotations;
using Zenject;

namespace Game.StateManagement
{
    [UsedImplicitly]
    public class GameRunningState : BaseGameState
    {
        [Inject] private GameContainer _gameContainer;

        public override void Enter()
        {
            base.Enter();

            PlayerProfileService.SetScore(0);
            PlayerProfileService.SetTotalLives(GameConfig.TotalLives);


            GameplayView menu = UiManager.ShowMenu<GameplayView>();
            menu.DisplayScore(PlayerProfileService.GetScore());
            menu.SetTitle(string.Empty);
            menu.DisplayPlayerLivesUI(GameConfig.TotalLives);

            _gameContainer.StartGame(true);
        }

        public override void Exit()
        {
            _gameContainer.StartGame(false);
        }
    }
}