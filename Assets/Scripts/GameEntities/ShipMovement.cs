using Asteroids.Game.Core;
using UnityEngine;

namespace Asteroids.Game.Runtime
{
    public class ShipMovement : GameEntity
    {
        [SerializeField] private float maxThrust = 10;
        [SerializeField] private float rotateSpeed = 135f;
        [SerializeField] private Rigidbody2D shipRigidbody2D;
        [SerializeField] private GameEntity bullet;
        [SerializeField] private float nextBulletSpawnTime = 1f;

        private float _currentTime;

        public override void Initialize()
        {
            base.Initialize();
        
            SetDirection(new Vector2(0, 1));
            _currentTime = 0;
        }

        public override void UpdateEntity()
        {
            var horizontal = Input.GetAxis("Horizontal");

            if (horizontal != 0)
            {
                var sign = Mathf.Sign(horizontal) * -1f;
                transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed * sign);
                SetDirection(transform.TransformDirection(Vector3.up));
            }

            if (Input.GetKeyDown(KeyCode.Z) && Time.time - _currentTime > nextBulletSpawnTime)
            {
                var position = transform.TransformPoint(new Vector2(0, 0.6f));
                var obj = PrefabHolder.instance.InstantiatePlayerBullet(position);
                obj.SetDirection(MoveDirection);

                _currentTime = Time.time;
            }
        }

        private void FixedUpdate()
        {
            var _addThrust = Input.GetAxis("Vertical") != 0;
            if (_addThrust && shipRigidbody2D != null)
            {
                shipRigidbody2D.AddForce(MoveDirection * maxThrust, ForceMode2D.Force);
            }
        }
    }
}