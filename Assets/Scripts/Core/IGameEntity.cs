namespace Asteroids.Game.Core
{
    public interface IGameEntity
    {
        void SetDirection(UnityEngine.Vector2 direction);
        void Initialize();
        void UpdateEntity();
        void DisposeEntity();
        void SetVisibility(bool isVisible);
        UnityEngine.GameObject GameObject { get; }

        void FixedUpdateEntity();
    }

    public interface IGame
    {
        void AddGameEntity(IGameEntity gameEntity);
        void RemoveGameEntity(IGameEntity gameEntity);
        void OnStateChanged(IGameState state);
        void UpdateGame();
        void OnFixedUpdate();
    }

    public interface IGameState
    {
        void Permit();
        void Execute();
    }
}