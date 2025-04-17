using Game.Configurations;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Engine.Core
{
    [UsedImplicitly]
    public class EnemySpawnController
    {
        private IConfigCollectionService _configService;
        private int _currentWave;

        private GameConfig _gameConfig;
        private GameEntitySpawnController _spawnController;
        private float _timeStep;
        private int _waveEnemiesCount;

        [Inject]
        private void Init(IConfigCollectionService configService,
            GameEntitySpawnController spawnController)
        {
            _configService = configService;
            _spawnController = spawnController;
        }

        public void OnUpdate()
        {
            if (_gameConfig == null)
            {
                _gameConfig = _configService.GameConfig;
                return;
            }

            Wave wave = _gameConfig.EnemyWaves[_currentWave];
            if (Time.time - _timeStep > wave.Delay)
            {
                if (_waveEnemiesCount > wave.Count)
                {
                    _waveEnemiesCount = 0;
                    _currentWave++;

                    if (_currentWave > _gameConfig.EnemyWaves.Length - 1)
                        _currentWave = 0;

                    _timeStep = Time.time + 10f;
                }

                float degrees = Random.Range(0, 360f) * Mathf.Deg2Rad;
                float radius = Random.Range(10f, 15f);
                Vector2 pos = new Vector2(Mathf.Cos(degrees) * radius, Mathf.Sin(degrees) * radius);

                string entityId = wave.Enemies[Random.Range(0, wave.Enemies.Length)];
                _spawnController.InstantiateEntity(entityId, pos);

                _waveEnemiesCount += 1;

                _timeStep = Time.time;
            }
        }

        public void Clear()
        {
            _currentWave = 0;
            _waveEnemiesCount = 0;
            _timeStep = 0;
        }
    }
}