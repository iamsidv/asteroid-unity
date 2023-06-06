using Asteroids.Game.Core;
using UnityEngine;

namespace Asteroids.Game.Runtime
{
    public class Bullet : GameEntity
    {
        public float speed = 4;
        public float timeToDestroy = 4f;
        public float timestep;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Asteroid"))
            {
                var entity = collision.gameObject.GetComponent<GameEntity>();
                if (entity != null)
                    entity.OnDestroy();

                OnDestroy();
            }
        }

        public override void UpdateEntity()
        {
            transform.position += speed * Time.deltaTime * MoveDirection;

            timestep += Time.deltaTime;
            if (timestep > timeToDestroy)
            {
                OnDestroy();
                timestep = 0;
            }
        }
    }
}