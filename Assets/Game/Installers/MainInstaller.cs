using Asteroids.Game.Core;
using Game.AssetManagement;
using Game.Configurations;
using Game.Core;
using Game.Engine;
using Game.PlayerState;
using Game.Services;
using Game.Signals;
using Game.StateManagement;
using Game.UI;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class MainInstaller : MonoInstaller<MainInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindFactory<Object, GameEntity, GameEntity.Factory>()
                .FromFactory<PrefabFactory<GameEntity>>().WhenInjectedInto<GameEntitySpawnService>();

            Container.BindInterfacesAndSelfTo<SignalService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameContainer>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyWavesSpawnService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameEntitySpawnService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ConfigCollectionService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerProfileService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameLoop>().AsSingle().WhenInjectedInto<GameContainer>();

            Container.Bind<MenuManager>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameStateManager>().AsSingle();
            Container.Bind<IGameState>().To<GameReadyState>().AsSingle().WhenInjectedInto<GameStateManager>();
            Container.Bind<IGameState>().To<GameLoadState>().AsSingle().WhenInjectedInto<GameStateManager>();
            Container.Bind<IGameState>().To<GameRunningState>().AsSingle().WhenInjectedInto<GameStateManager>();
            Container.Bind<IGameState>().To<GameOverState>().AsSingle().WhenInjectedInto<GameStateManager>();
        }
    }
}