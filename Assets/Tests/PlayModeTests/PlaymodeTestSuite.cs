using System.Collections;
using Game.AssetManagement;
using Game.Configurations;
using Game.Engine;
using Game.Engine.Core;
using Game.Engine.Entities;
using Game.Managers;
using Game.PlayerState;
using Game.Signals;
using Game.StateManagement;
using Game.UI;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.TestTools;
using Zenject;

namespace Tests.PlayModeTests
{
    public class PlaymodeTestSuite : ZenjectIntegrationTestFixture
    {
        [UnityTest]
        public IEnumerator UT_PlayModeLoadAddressableTest()
        {
            PreInstall();
            PostInstall();

            AsyncOperationHandle<GameConfig> operation = Addressables.LoadAssetAsync<GameConfig>("Assets/Config/GameConfig.asset");

            Assert.That(operation.IsValid);

            GameConfig isComplete = null;

            operation.Completed += (handle) => { isComplete = handle.Result; };

            while (isComplete == null)
            {
                yield return null;
            }

            Assert.IsNotNull(isComplete);
        }

        [UnityTest]
        public IEnumerator UT_TestDeductPlayerLifeOnAsteriodCollision()
        {
            GameConfig gameConfig = AssetDatabase.LoadAssetAtPath<GameConfig>("Assets/Config/GameConfig.asset");

            CommonPlayerShipInstallBindings<Asteroid>(gameConfig, gameConfig.GameElements[0].Prefab.gameObject);

            PlayerProfileService profileService = Container.Resolve<PlayerProfileService>();
            profileService.SetTotalLives(3);
            int totallives = profileService.GetTotalLives();

            PlayerShip ship = Container.Resolve<PlayerShip>();
            Asteroid asteroid = Container.Resolve<Asteroid>();
            asteroid.SetVisibility(true);

            ship.transform.position = asteroid.transform.position = new Vector3(0, 0, 0);

            yield return null;

            Assert.Less(profileService.GetTotalLives(), totallives);
        }

        [UnityTest]
        public IEnumerator UT_TestDeductPlayerLifeOnSaucerCollision()
        {
            GameConfig gameConfig = AssetDatabase.LoadAssetAtPath<GameConfig>("Assets/Config/GameConfig.asset");

            CommonPlayerShipInstallBindings<EnemySaucer>(gameConfig, gameConfig.GameElements[3].Prefab.gameObject);

            PlayerProfileService profileService = Container.Resolve<PlayerProfileService>();
            profileService.SetTotalLives(3);
            int totallives = profileService.GetTotalLives();

            PlayerShip ship = Container.Resolve<PlayerShip>();
            EnemySaucer saucer = Container.Resolve<EnemySaucer>();
            saucer.SetVisibility(true);

            ship.transform.position = saucer.transform.position = new Vector3(0, 0, 0);

            yield return null;

            Assert.Less(profileService.GetTotalLives(), totallives);
        }

        [UnityTest]
        public IEnumerator UT_TestDeductPlayerLifeOnEnemyProjectileCollision()
        {
            GameConfig gameConfig = AssetDatabase.LoadAssetAtPath<GameConfig>("Assets/Config/GameConfig.asset");

            CommonPlayerShipInstallBindings<Bullet>(gameConfig, gameConfig.EnemyProjectile.gameObject);

            PlayerProfileService profileService = Container.Resolve<PlayerProfileService>();
            profileService.SetTotalLives(3);
            int totalLives = profileService.GetTotalLives();

            PlayerShip ship = Container.Resolve<PlayerShip>();
            Bullet bullet = Container.Resolve<Bullet>();
            bullet.SetVisibility(true);

            ship.transform.position = bullet.transform.position = new Vector3(0, 0, 0);

            yield return null;

            Assert.Less(profileService.GetTotalLives(), totalLives);
        }

        private void CommonPlayerShipInstallBindings<TComponent>(GameConfig gameConfig, GameObject mockGameObject)
        {
            Camera cam = new GameObject("Main Camera").AddComponent<Camera>();
            cam.tag = "MainCamera";

            PreInstall();

            // Call Container.Bind methods
            Container.Bind<MainManager>().FromNewComponentOnNewGameObject().AsSingle();
            Container.Bind<PlayerShip>().FromComponentInNewPrefab(gameConfig.PlayerShip).AsSingle();
            Container.Bind<TComponent>().FromComponentInNewPrefab(mockGameObject).AsSingle();

            Container.BindFactory<Object, GameEntity, GameEntity.Factory>()
                .FromFactory<PrefabFactory<GameEntity>>().WhenInjectedInto<GameEntitySpawnController>();

            Container.BindInterfacesAndSelfTo<SignalService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameContainer>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawnController>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameEntitySpawnController>().AsSingle();
            Container.BindInterfacesAndSelfTo<ConfigCollectionService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerProfileService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStateManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameLoop>().AsSingle().WhenInjectedInto<GameContainer>();

            Container.Bind<UiManager>().AsSingle();

            PostInstall();

            Container.Resolve<MainManager>();
        }
    }
}