using Asteroids.Game.Core;
using Asteroids.Game.Signals;
using UnityEngine;

namespace Asteroids.Game.Runtime
{
    public class Bullet : GameEntity
    {
        public float speed = 4;
        public float timeToDestroy = 4f;
        public float timestep;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Asteroid"))
            {
                var entity = collision.gameObject.GetComponent<GameEntity>();
                if (entity != null)
                {
                    if (this.gameObject.CompareTag("PlayerBullet"))
                    {
                        SignalService.Publish(new UpdateScoreSignal { Value = entity.Score });
                    }
                    entity.DisposeEntity();
                }
                DisposeEntity();
            }
        }

        public override void EntityUpdate()
        {
            transform.position += speed * Time.deltaTime * MoveDirection;

            timestep += Time.deltaTime;
            if (timestep > timeToDestroy)
            {
                DisposeEntity();
                timestep = 0;
            }
        }
    }
}