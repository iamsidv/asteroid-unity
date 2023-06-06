using Asteroids.Game.Runtime;
using UnityEngine;

namespace Asteroids.Game.Core
{
    public abstract class GameEntity : MonoBehaviour, IGameEntity
    {
        private Vector2 _direction;

        public GameObject GameObject => gameObject;
        protected Vector3 MoveDirection => _direction;

        public abstract void UpdateEntity();

        private void Start()
        {
            ContainerManager.AddEntity(this);
        }

        public virtual void Initialize()
        {
        }

        public virtual void DisposeEntity()
        {
            ContainerManager.RemoveEntity(this);
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

        public virtual void FixedUpdateEntity()
        {
        }
    }
}