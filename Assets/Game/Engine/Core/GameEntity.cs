using Asteroids.Game.Core;
using Asteroids.Game.Services;
using Game.Services;
using Game.Signals;
using UnityEngine;
using Zenject;

namespace Game.Core
{
    public abstract class GameEntity : MonoBehaviour, IGameEntity
    {
        private Vector2 _direction;

        private IGameContainer _gameContainer;
        protected ISignalService _signalService;
        protected GameEntitySpawnService _spawnService;
        public int DieScore { get; private set; }

        protected Vector3 MoveDirection => _direction;

        public GameObject GameObject => gameObject;

        public virtual void EntityStart()
        {
        }

        public abstract void EntityUpdate();

        public virtual void EntityFixedUpdate()
        {
        }

        public virtual void DisposeEntity()
        {
            _gameContainer.RemoveEntity(this);
            Destroy(gameObject);
        }

        public void SetVisibility(bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        public void OnInitialize(int score)
        {
            DieScore = score;
        }

        [Inject]
        private void InitServices(IGameContainer gameContainer,
            GameEntitySpawnService spawnManager,
            ISignalService signalService)
        {
            _gameContainer = gameContainer;
            _spawnService = spawnManager;
            _signalService = signalService;
            _gameContainer.AddEntity(this);
        }

        public class Factory : PlaceholderFactory<Object, GameEntity>
        {
        }
    }

    public class GameEntityFactory : IFactory<Object, GameEntity>
    {
        private readonly DiContainer _container;

        public GameEntityFactory(DiContainer container)
        {
            _container = container;
        }

        public GameEntity Create(Object param)
        {
            return _container.InstantiatePrefabForComponent<GameEntity>(param);
        }
    }
}