using Asteroids.Game.Core;
using Asteroids.Game.Signals;
using System;
using UnityEngine;

namespace Asteroids.Game.Runtime
{
    public class ContainerManager : MonoBehaviour
    {
        private IGame game;
        private static ContainerManager instance;
        public GameState gameState;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            game = new GameLoop();
        }

        private void OnEnable()
        {
            SignalService.Subscribe<GameStateUpdateSignal>(SetGameState);
        }

        private void SetGameState(GameStateUpdateSignal signal)
        {
            gameState = signal.Value;

            if(gameState == GameState.GameOver)
            {
                game.OnStateChanged(null);
            }
        }

        private void OnDisable()
        {
            SignalService.RemoveSignal<GameStateUpdateSignal>(SetGameState);
        }

        public static void AddEntity(IGameEntity entity)
        {
            if (instance == null)
            {
                Debug.Log("Cannot Add Entity as Container Instance is empty");
                return;
            }

            if (instance.game == null)
            {
                Debug.Log("Cannot Add Entity as Game is not created");
                return;
            }

            entity.EntityStart();

            instance.game.AddGameEntity(entity);
        }

        public static void RemoveEntity(IGameEntity entity)
        {
            if (instance == null)
            {
                Debug.Log("Cannot Add Entity as Container Instance is empty");
                return;
            }

            if (instance.game == null)
            {
                Debug.Log("Cannot Add Entity as Game is not created");
                return;
            }

            instance.game.RemoveGameEntity(entity);
        }

        private void Update()
        {
            if (gameState != GameState.Running)
                return;

            if (instance.game != null)
                instance.game.UpdateGame();
        }

        private void FixedUpdate()
        {
            if (gameState != GameState.Running)
                return;

            if (instance.game != null)
                instance.game.OnFixedUpdate();
        }
    }
}