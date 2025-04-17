using Game.Engine.Core;
using UnityEngine;

namespace Game.Engine.Entities
{
    public class Asteroid : NonPlayableMovableEntity
    {
        private enum AsteroidSize
        {
            Large,
            Medium,
            Small
        }
        
        [SerializeField] private AsteroidSize sizeType;
        [SerializeField] private string[] spawnOnDestroyIds;

        public override void EntityUpdate()
        {
            MoveEntity();
        }

        public override void DisposeEntity()
        {
            SplitEntity();
            base.DisposeEntity();
        }

        private void SplitEntity()
        {
            for (int i = 0; i < spawnOnDestroyIds?.Length; i++)
            {
                SpawnController.InstantiateEntity(spawnOnDestroyIds[i], transform.position);
            }
        }
    }
}