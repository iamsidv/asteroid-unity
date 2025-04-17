using Game.Engine.Core;
using UnityEngine;

namespace Game.Engine.Entities
{
    public class EnemySaucer : NonPlayableMovableEntity
    {
        [SerializeField] private float shootDelay;
        [SerializeField] private float updateDirectionDelay;
        [SerializeField] private GameEntity bulletEntity;
        private float _directionTimeStep;

        private float _timeStep;

        public override void EntityUpdate()
        {
            MoveEntity();

            if (Time.time - _timeStep > shootDelay)
            {
                GenerateProjectile();
                _timeStep = Time.time;
            }

            if (updateDirectionDelay > 0 && Time.time - _directionTimeStep > updateDirectionDelay)
            {
                SetDirection(Random.insideUnitCircle.normalized);
                _directionTimeStep = Time.time;
            }
        }

        private void GenerateProjectile()
        {
            Vector2 direction = Random.insideUnitCircle.normalized;
            Vector3 position = transform.position + (Vector3)direction;

            IGameEntity obj = SpawnController.InstantiateEnemyBullet(position);
            obj.SetDirection(direction);
        }
    }
}