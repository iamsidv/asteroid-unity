using System.Collections.Generic;
using System.Threading.Tasks;
using Game.AssetManagement;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceLocations;
using Zenject;

namespace Game.UI
{
    [UsedImplicitly]
    public class UiManager
    {
        private readonly List<BaseView> _views = new();

        private IAssetProvider _assetProvider;
        private DiContainer _container;

        [Inject]
        private void InitManager(IAssetProvider assetProvider, DiContainer container)
        {
            _assetProvider = assetProvider;
            _container = container;
        }
        
        public T ShowMenu<T>() where T : BaseView
        {
            T menu = GetMenu<T>();
            menu.SetVisibility(true);
            menu.OnScreenEnter();
            return menu;
        }

        public T GetMenu<T>() where T : BaseView
        {
            BaseView menu = _views.Find(t => t.GetType() == typeof(T));
            return menu as T;
        }

        public void HideMenu<T>() where T : BaseView
        {
            T menu = GetMenu<T>();
            menu.OnScreenExit();
            menu.SetVisibility(false);
        }

        public async Task LoadMenus(IList<IResourceLocation> uiResourceLocations)
        {
            foreach (IResourceLocation resourceLocation in uiResourceLocations)
            {
                GameObject go = await _assetProvider.LoadAssetAsync<GameObject>(resourceLocation.PrimaryKey);
                BaseView viewInstance = _container.InstantiatePrefabForComponent<BaseView>(go);
                _views.Add(viewInstance);
            }
        }
    }
}