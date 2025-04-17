using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Engine.Core
{
    [UsedImplicitly]
    public class GameLoop : IGameLoop
    {
        private readonly EnemySpawnController _enemySpawnController;
        private readonly List<IGameEntity> _gameEntities;
        private Vector3 _bottomLeftPoint;
        private Vector3 _topRightPoint;

        public GameLoop(EnemySpawnController enemySpawnController)
        {
            _gameEntities = new List<IGameEntity>();
            _enemySpawnController = enemySpawnController;
            SetCameraBounds();
        }

        public void AddGameEntity(IGameEntity gameEntity)
        {
            _gameEntities.Add(gameEntity);
        }

        public void DisposeGameEntities()
        {
            foreach (IGameEntity item in _gameEntities)
            {
                GameObject.Destroy(item.GameObject);
            }

            _gameEntities.Clear();
            _enemySpawnController.Clear();
        }

        public void RemoveGameEntity(IGameEntity gameEntity)
        {
            if (_gameEntities.Contains(gameEntity))
            {
                _gameEntities.Remove(gameEntity);
            }
        }

        public void UpdateFrame()
        {
            _enemySpawnController.OnUpdate();

            for (int i = 0; i < _gameEntities?.Count; i++)
            {
                IGameEntity entity = _gameEntities[i];
                entity.EntityUpdate();
                HandleScreenWarp(entity.GameObject.transform);
            }
        }

        public void FixedUpdateFrame()
        {
            for (int i = 0; i < _gameEntities?.Count; i++)
            {
                _gameEntities[i].EntityFixedUpdate();
            }
        }

        private void SetCameraBounds()
        {
            float cameraZ = Camera.main.transform.position.z;
            _bottomLeftPoint = Camera.main.ScreenToWorldPoint(Vector3.zero - new Vector3(0, 0, cameraZ));
            _topRightPoint =
                Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height) - new Vector3(0, 0, cameraZ));
        }

        private void HandleScreenWarp(Transform target)
        {
            if (target != null)
            {
                Vector3 pos = target.position;
                Vector3 offset = target.localScale * 0.5f;

                if (pos.x < _bottomLeftPoint.x - offset.x)
                {
                    pos.x = _topRightPoint.x + offset.x;
                }
                else if (pos.x > _topRightPoint.x + offset.x)
                {
                    pos.x = _bottomLeftPoint.x - offset.x;
                }

                if (pos.y < _bottomLeftPoint.y - offset.y)
                {
                    pos.y = _topRightPoint.y + offset.y;
                }
                else if (pos.y > _topRightPoint.y + offset.y)
                {
                    pos.y = _bottomLeftPoint.y - offset.y;
                }

                target.position = pos;
            }
        }
    }
}