using System.Collections;
using Game.Engine.Core;
using Game.Signals;
using UnityEngine;

namespace Game.Engine.Entities
{
    public class PlayerShip : GameEntity
    {
        [SerializeField] private float maxThrust = 10;
        [SerializeField] private float rotateSpeed = 135f;
        [SerializeField] private Rigidbody2D shipRigidbody2D;
        [SerializeField] private Collider2D shipCollider2D;
        [SerializeField] private SpriteRenderer renderer2D;
        [SerializeField] private GameEntity bullet;
        [SerializeField] private float nextBulletSpawnTime = 1f;

        private float _currentTime;
        private bool _isReviving;

        private void OnEnable()
        {
            SignalService.Subscribe<PlayerReviveSignal>(OnPlayerShipRevived);
        }

        private void OnDisable()
        {
            SignalService.RemoveSignal<PlayerReviveSignal>(OnPlayerShipRevived);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_isReviving)
                return;

            SignalService.Publish<PlayerDiedSignal>();
            shipCollider2D.enabled = false;
            renderer2D.enabled = false;
            _isReviving = true;
        }

        public override void EntityStart()
        {
            base.EntityStart();

            SetDirection(new Vector2(0, 1));
            _currentTime = 0;
        }

        public override void EntityUpdate()
        {
            if (_isReviving)
                return;

            RotateShip();

            if (Input.GetKeyDown(KeyCode.Z) && Time.time - _currentTime > nextBulletSpawnTime)
            {
                SpawnBullet();
                _currentTime = Time.time;
            }
        }

        public override void EntityFixedUpdate()
        {
            if (_isReviving)
                return;

            AddThrust();
        }

        private void RotateShip()
        {
            float horizontal = Input.GetAxis("Horizontal");

            if (horizontal != 0)
            {
                float sign = Mathf.Sign(horizontal) * -1f;
                transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed * sign);
                SetDirection(transform.TransformDirection(Vector3.up));
            }
        }

        private void SpawnBullet()
        {
            Vector3 position = transform.TransformPoint(new Vector2(0, 0.6f));
            IGameEntity playerBullet = SpawnController.InstantiatePlayerBullet(position);
            playerBullet.SetDirection(MoveDirection);
        }

        private void AddThrust()
        {
            bool addThrust = Input.GetAxis("Vertical") != 0;
            if (addThrust && shipRigidbody2D != null)
            {
                shipRigidbody2D.AddForce(MoveDirection * maxThrust, ForceMode2D.Force);
            }
        }

        private void OnPlayerShipRevived(PlayerReviveSignal signal)
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            shipRigidbody2D.velocity = Vector3.zero;

            renderer2D.enabled = true;
            StartCoroutine(PlayReviveSequence());
        }

        private IEnumerator PlayReviveSequence()
        {
            for (int i = 0; i < 10; i++)
            {
                renderer2D.enabled = (i % 2 == 0);
                yield return new WaitForSeconds(0.1f);
            }

            renderer2D.enabled = true;
            yield return new WaitForSeconds(0.5f);
            _isReviving = false;
            shipCollider2D.enabled = true;
            SetDirection(new Vector3(0, 1));
        }
    }
}