using Game.Engine.Core;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Engine
{
    [UsedImplicitly]
    public class GameContainer : IGameContainer, ITickable, IFixedTickable
    {
        private IGameLoop _gameLoop;

        private bool _isInitialized;

        [Inject]
        private void InitContainer(IGameLoop gameLoop)
        {
            _gameLoop = gameLoop;
        }
        
        public void FixedTick()
        {
            if (!_isInitialized)
            {
                return;
            }

            _gameLoop?.FixedUpdateFrame();
        }

        public void AddEntity(IGameEntity entity)
        {
            if (_gameLoop == null)
            {
                Debug.Log("Cannot Add Entity as Game is not created");
                return;
            }

            entity.EntityStart();

            _gameLoop.AddGameEntity(entity);
        }

        public void RemoveEntity(IGameEntity entity)
        {
            if (_gameLoop == null)
            {
                Debug.Log("Cannot Add Entity as Game is not created");
                return;
            }

            _gameLoop.RemoveGameEntity(entity);
        }

        public void Tick()
        {
            if (!_isInitialized)
            {
                return;
            }

            _gameLoop?.UpdateFrame();
        }

        public void StartGame(bool startGame)
        {
            _isInitialized = startGame;
        }

        public void CleanupGameEntities()
        {
            _gameLoop?.DisposeGameEntities();
        }
    }
}