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
        protected IConfigCollectionService ConfigCollection;
        protected IPlayerProfileService PlayerProfileService;
        protected UiManager UiManager;

        protected GameConfig GameConfig => ConfigCollection.GameConfig;

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        [Inject]
        private void InitService(IConfigCollectionService configCollection,
            IPlayerProfileService playerProfileService,
            UiManager uiManager)
        {
            ConfigCollection = configCollection;
            PlayerProfileService = playerProfileService;
            UiManager = uiManager;
        }
    }
}