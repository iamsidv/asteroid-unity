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
        [Inject] private GameContainer _gameContainer;
        [Inject] private GameStateManager gameStateManager;

        public void CancelOperation()
        {
            _cancellationTokenSource?.Cancel();
        }

        public override void Enter()
        {
            base.Enter();

            _gameContainer.CleanupGameEntities();

            MenuManager.HideMenu<MainMenuView>();
            GameplayView menu = MenuManager.GetMenu<GameplayView>();
            menu.DisplayScore(PlayerProfileService.GetScore());
            menu.SetTitle("Gameover");
            menu.Clear();

            _cancellationToken = _cancellationTokenSource.Token;
            _ = AsyncStartNewGame(3f);
        }

        private async Task AsyncStartNewGame(float delay)
        {
            await Task.Delay(TimeSpan.FromSeconds(delay), _cancellationToken);
            gameStateManager.SetState<GameReadyState>();
        }
    }
}