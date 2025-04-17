using System.Collections.Generic;
using JetBrains.Annotations;
using Zenject;

namespace Game.StateManagement
{
    [UsedImplicitly]
    public class GameStateManager
    {
        [Inject] private List<IGameState> _gameStates;

        private IGameState _currentState;

        public void SetState<T>() where T : IGameState
        {
            foreach (IGameState state in _gameStates)
            {
                if (state is T newState)
                {
                    ChangeState(newState);
                    break;
                }
            }
        }
        
        public void CancelCurrentStateOp()
        {
            if (_currentState is ICancellableOp cancellableState)
            {
                cancellableState.CancelOperation();
            }
        }

        private void ChangeState(IGameState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
    }
}