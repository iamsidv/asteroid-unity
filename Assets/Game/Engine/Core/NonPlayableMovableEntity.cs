using UnityEngine;

namespace Game.Engine.Core
{
    public abstract class NonPlayableMovableEntity : GameEntity
    {
        [SerializeField] private float speed = 4f;

        public override void EntityStart()
        {
            base.EntityStart();
            SetDirection(Random.insideUnitCircle.normalized);
        }

        protected void MoveEntity()
        {
            transform.position += speed * Time.deltaTime * MoveDirection;
        }
    }
}