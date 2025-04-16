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
        protected MenuManager MenuManager;
        protected IPlayerProfileService PlayerProfileService;

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
            MenuManager menuManager)
        {
            _configService = configService;
            PlayerProfileService = playerProfileService;
            MenuManager = menuManager;
        }
    }
}