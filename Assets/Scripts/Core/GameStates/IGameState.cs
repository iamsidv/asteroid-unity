using Asteroids.Game.Config;
using Asteroids.Game.Services;
using Zenject;

namespace Asteroids.Game.Core
{
    public interface IGameState
    {
        void Execute();
    }

    public class BaseGameState : IGameState
    {
        private IConfigCollectionService _configService;
        protected IPlayerProfileService _playerProfileService;

        [Inject]
        private void InitService(IConfigCollectionService configService, 
            IPlayerProfileService playerProfileService)
        {
            _configService = configService;
            _playerProfileService = playerProfileService;
        }

        protected GameConfig GameConfig => _configService.GameConfig;

        public virtual void Execute()
        {
        }
    }
}