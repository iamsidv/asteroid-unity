using Game.Configurations;
using Game.Signals;
using UnityEngine;
using Zenject;

namespace Game.Services
{
    public class EnemyWavesSpawnService : IInitializable
    {
        private IConfigCollectionService _configService;
        private int _currentWave;
        private GameConfig _gameConfig;
        private GameEntitySpawnService _spawnService;
        private float _timeStep;
        private int _waveEnemiesCount;

        public void Initialize()
        {
        }

        [Inject]
        private void Init(IConfigCollectionService configService,
            ISignalService signalService,
            GameEntitySpawnService spawnService)
        {
            _configService = configService;
            _spawnService = spawnService;
        }

        public void OnUpdate()
        {
            if (_gameConfig == null)
            {
                _gameConfig = _configService.GameConfig;
                return;
            }

            var wave = _gameConfig.EnemyWaves[_currentWave];
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

                var degrees = Random.Range(0, 360f) * Mathf.Deg2Rad;
                var radius = Random.Range(10f, 15f);
                var pos = new Vector2(Mathf.Cos(degrees) * radius, Mathf.Sin(degrees) * radius);

                var entityId = wave.Enemies[Random.Range(0, wave.Enemies.Length)];
                _spawnService.InstantiateEntity(entityId, pos);

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