using Asteroids.Game.Core;
using Asteroids.Game.Services;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Engine
{
    [UsedImplicitly]
    public class GameContainer : IGameContainer, ITickable, IFixedTickable
    {
        private IGameLoop _currentGame;

        [Inject]
        private void InitContainer(IGameLoop gameLoop)
        {
            _currentGame = gameLoop;
        }

        private bool _isInitialized;
        
        public void StartGame(bool startGame)
        {
            _isInitialized = startGame;
        }

        public void Tick()
        {
            if (!_isInitialized)
                return;

            if (_currentGame != null)
                _currentGame.UpdateFrame();
        }

        public void FixedTick()
        {
            if (!_isInitialized)
                return;

            if (_currentGame != null)
                _currentGame.FixedUpdateFrame();
        }

        public void AddEntity(IGameEntity entity)
        {
            if (_currentGame == null)
            {
                Debug.Log("Cannot Add Entity as Game is not created");
                return;
            }

            entity.EntityStart();

            _currentGame.AddGameEntity(entity);
        }

        public void RemoveEntity(IGameEntity entity)
        {
            if (_currentGame == null)
            {
                Debug.Log("Cannot Add Entity as Game is not created");
                return;
            }

            _currentGame.RemoveGameEntity(entity);
        }

        public void CleanupGameEntities()
        {
            _currentGame?.DisposeGameEntities();
        }
    }
}