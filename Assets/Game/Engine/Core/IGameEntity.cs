using UnityEngine;

namespace Game.Engine.Core
{
    public interface IGameEntity
    {
        GameObject GameObject { get; }

        void SetDirection(Vector2 direction);
        void OnInitialize(int score);
        void EntityStart();
        void EntityUpdate();
        void EntityFixedUpdate();
        void DisposeEntity();
        void SetVisibility(bool isVisible);
    }
}