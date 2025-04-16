using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Game.AssetManagement
{
    public interface IAssetProvider
    {
        void LoadAssetWithCallback<TAsset>(AssetReference assetReference, Action<TAsset> callback);
        Task<TAsset> LoadAssetAsync<TAsset>(string assetPath);
        Task<IList<IResourceLocation>> LoadAssetLabels(IReadOnlyList<string> keys);
    }
}