using Game.Signals;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Engine.Core
{
    public abstract class GameEntity : MonoBehaviour, IGameEntity
    {
        private Vector2 _direction;
        private IGameContainer _gameContainer;

        protected ISignalService SignalService;
        protected GameEntitySpawnController SpawnController;
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
            GameEntitySpawnController spawnManager,
            ISignalService signalService)
        {
            _gameContainer = gameContainer;
            SpawnController = spawnManager;
            SignalService = signalService;
            _gameContainer.AddEntity(this);
        }

        [UsedImplicitly]
        public class Factory : PlaceholderFactory<Object, GameEntity>
        {
        }
    }
}