namespace Asteroids.Game.Core
{
    public interface IGameEntity
    {
        void Init(UnityEngine.Vector2 direction);
        void Initialize();
        void UpdateEntity();
        void OnDestroy();
        void SetVisibility(bool isVisible);
        UnityEngine.GameObject GameObject { get; }
    }

    public interface IGame
    {
        void AddGameEntity(IGameEntity gameEntity);
        void RemoveGameEntity(IGameEntity gameEntity);
        void OnStateChanged(IGameState state);
    }

    public interface IGameState
    {
        void Permit();
        void Execute();
    }
}