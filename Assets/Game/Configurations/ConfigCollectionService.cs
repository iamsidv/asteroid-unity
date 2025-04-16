using JetBrains.Annotations;

namespace Game.Configurations
{
    [UsedImplicitly]
    public class ConfigCollectionService : IConfigCollectionService
    {
        public GameConfig GameConfig { get; private set; }

        public void SetGameConfig(GameConfig gameConfig)
        {
            GameConfig = gameConfig;
        }
    }
}