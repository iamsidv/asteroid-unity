using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Game.Core
{
    public abstract class GameEntity : MonoBehaviour, IGameEntity
    {
        protected Vector2 _direction;

        public GameObject GameObject => gameObject;

        public abstract void Initialize();
        public abstract void UpdateEntity();

        public virtual void OnDestroy()
        {
            Destroy(gameObject);
        }

        public void SetVisibility(bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }

        public void Init(Vector2 direction)
        {
            _direction = direction;
        }
    }
}
