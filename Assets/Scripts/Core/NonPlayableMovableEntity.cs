using Asteroids.Game.Core;
using UnityEngine;

namespace Asteroids.Game.Core
{
    public class NonPlayableMovableEntity : GameEntity
    {
        [SerializeField] protected float speed = 4f;

        public override void Initialize()
        {
           
        }

        public override void UpdateEntity()
        {
            
        }

        private void Start()
        {
            _direction = Random.insideUnitCircle.normalized;
        }

        //private void Update()
        //{
        //    transform.position += speed * Time.deltaTime * (Vector3)_direction;
        //}
    }
}