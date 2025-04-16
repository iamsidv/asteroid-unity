using Game.Configurations;
using Game.PlayerState;
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

        [Inject]
        private void InitService(IConfigCollectionService configService,
            IPlayerProfileService playerProfileService)
        {
            _configService = configService;
            PlayerProfileService = playerProfileService;
        }

        protected GameConfig GameConfig => _configService.GameConfig;

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }
    }
}