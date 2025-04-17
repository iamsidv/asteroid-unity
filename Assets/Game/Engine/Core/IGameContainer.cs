namespace Game.Engine.Core
{
    public interface IGameContainer
    {
        void AddEntity(IGameEntity entity);
        void RemoveEntity(IGameEntity entity);
    }
}