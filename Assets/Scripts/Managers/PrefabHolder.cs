using Asteroids.Game.Core;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PrefabHolder : MonoBehaviour
{
    public static PrefabHolder instance;

    public int totalLives = 5;
    public GameplayElement[] gameplayElements;
    public GameEntity playerBullet;
    public GameEntity enemyBullet;

    public Wave[] waves;

    public int currentWave;
    public int waveEnemiesCount;
    public float timeStep;

    public GameObject playerShip;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (MainManager.CurrentGameState != GameState.Running)
            return;

        var wave = waves[currentWave];
        if (Time.time - timeStep > wave.delay)
        {
            if (waveEnemiesCount > wave.count)
            {
                waveEnemiesCount = 0;
                currentWave++;

                if (currentWave > waves.Length - 1)
                    currentWave = 0;

                timeStep = Time.time + 10f;
            }


            var degrees = Random.Range(0, 360f) * Mathf.Deg2Rad;
            var radius = Random.Range(10f, 15f);
            var pos = new Vector2(Mathf.Cos(degrees) * radius, Mathf.Sin(degrees) * radius);

            var entityId = wave.enemies[Random.Range(0, wave.enemies.Length)];
            InstantiateEntity(entityId, pos);

            waveEnemiesCount += 1;

            timeStep = Time.time;
        }
    }

    public IGameEntity InstantiatePlayerBullet(Vector2 position)
    {
        return InstantiateEntity(playerBullet, position);
    }

    public IGameEntity InstantiateEnemyBullet(Vector3 position)
    {
        return InstantiateEntity(enemyBullet, position);
    }

    public IGameEntity InstantiateEntity(string entityId, Vector2 position)
    {
        var entity = gameplayElements.First(t => t.id.Equals(entityId)).prefab;
        return InstantiateEntity(entity, position);
    }

    public IGameEntity InstantiateEntity(IGameEntity entity, Vector2 position)
    {
        var obj = Instantiate(entity as GameEntity, position, Quaternion.identity);
        obj.SetVisibility(true);
        return obj;
    }
}
