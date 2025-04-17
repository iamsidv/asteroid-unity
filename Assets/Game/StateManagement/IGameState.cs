using Game.Configurations;
using Game.PlayerState;
using Game.UI;
using Zenject;

namespace Game.StateManagement
{
    public interface IGameState
    {
        void Enter();
        void Exit();
    }

    public interface ICancellableOp
    {
        void CancelOperation();
    }

    public abstract class BaseGameState : IGameState
    {
        private IConfigCollectionService _configService;
        protected IPlayerProfileService PlayerProfileService;
        protected UiManager UiManager;

        protected GameConfig GameConfig => _configService.GameConfig;

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        [Inject]
        private void InitService(IConfigCollectionService configService,
            IPlayerProfileService playerProfileService,
            UiManager _uiManager)
        {
            _configService = configService;
            PlayerProfileService = playerProfileService;
            UiManager = _uiManager;
        }
    }
}