namespace Game.Configurations
{
    public interface IConfigCollectionService
    {
        public GameConfig GameConfig { get; }

        void SetGameConfig(GameConfig gameConfig);
    }
}