using Game.Engine.Core;
using Game.Signals;
using UnityEngine;

namespace Game.Engine.Entities
{
    public class Bullet : NonPlayableMovableEntity
    {
        [SerializeField] private float timeToDestroy = 4f;

        private bool _canUpdateScore;
        private float _timeStep;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Asteroid") || collision.gameObject.CompareTag("Saucer"))
            {
                GameEntity entity = collision.gameObject.GetComponent<GameEntity>();
                if (entity != null)
                {
                    if (_canUpdateScore)
                    {
                        SignalService.Publish(new UpdateScoreSignal { Value = entity.DieScore });
                    }

                    entity.DisposeEntity();
                }

                DisposeEntity();
            }
        }

        public override void EntityStart()
        {
            _canUpdateScore = gameObject.CompareTag("PlayerBullet");
        }

        public override void EntityUpdate()
        {
            MoveEntity();

            _timeStep += Time.deltaTime;
            if (_timeStep > timeToDestroy)
            {
                DisposeEntity();
                _timeStep = 0;
            }
        }
    }
}