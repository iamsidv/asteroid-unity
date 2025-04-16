using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Game.AssetManagement
{
    public interface IAssetProvider
    {
        void LoadAssetWithCallback<TAsset>(AssetReference assetReference, System.Action<TAsset> callback);
        Task<TAsset> LoadAssetAsync<TAsset>(string assetPath);
    }
}