using System.Linq;
using Asteroids.Game.Core;
using Game.Configurations;
using Game.Core;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Services
{
    [UsedImplicitly]
    public class GameEntitySpawnService
    {
        private IConfigCollectionService _configService;
        private GameEntity.Factory _factory;

        [Inject]
        private void Init(GameEntity.Factory factory,
            IConfigCollectionService configService)
        {
            _configService = configService;
            _factory = factory;
        }

        public void InstantiatePlayerShip()
        {
            _factory.Create(_configService.GameConfig.PlayerShip);
        }

        public IGameEntity InstantiateEnemyBullet(Vector3 position)
        {
            return InstantiateEntity(_configService.GameConfig.EnemyProjectile, position);
        }

        public IGameEntity InstantiatePlayerBullet(Vector3 position)
        {
            return InstantiateEntity(_configService.GameConfig.PlayerProjectile, position);
        }

        public IGameEntity InstantiateEntity(string entityId, Vector2 position)
        {
            var gameplayElement = _configService.GameConfig.GameElements.First(t => t.Id.Equals(entityId));
            var gameEntity = _factory.Create(gameplayElement.Prefab);
            gameEntity.gameObject.transform.position = position;
            gameEntity.OnInitialize(gameplayElement.Score);
            gameEntity.SetVisibility(true);
            return gameEntity;
        }

        private IGameEntity InstantiateEntity(IGameEntity prefab, Vector2 pos)
        {
            var gameEntity = _factory.Create(prefab.GameObject);
            gameEntity.gameObject.transform.position = pos;
            gameEntity.SetVisibility(true);
            return gameEntity;
        }
    }
}