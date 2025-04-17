using System;
using System.Threading;
using System.Threading.Tasks;
using Game.Engine;
using Game.UI.Gameplay;
using Game.UI.MainMenu;
using JetBrains.Annotations;
using Zenject;

namespace Game.StateManagement
{
    [UsedImplicitly]
    public class GameOverState : BaseGameState, ICancellableOp
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        private CancellationToken _cancellationToken;
        private GameContainer _gameContainer;
        private GameStateManager _gameStateManager;

        [Inject]
        private void InitState(GameContainer gameContainer,
            GameStateManager gameStateManager)
        {
            _gameContainer = gameContainer;
            _gameStateManager = gameStateManager;
        }
        
        public void CancelOperation()
        {
            _cancellationTokenSource?.Cancel();
        }

        public override void Enter()
        {
            base.Enter();

            _gameContainer.CleanupGameEntities();

            UiManager.HideMenu<MainMenuView>();
            GameplayView menu = UiManager.GetMenu<GameplayView>();
            menu.DisplayScore(PlayerProfileService.GetScore());
            menu.SetTitle("Game over");
            menu.Clear();

            _cancellationToken = _cancellationTokenSource.Token;
            _ = AsyncStartNewGame(3f);
        }

        private async Task AsyncStartNewGame(float delay)
        {
            await Task.Delay(TimeSpan.FromSeconds(delay), _cancellationToken);
            _gameStateManager.SetState<GameReadyState>();
        }
    }
}