using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Game.AssetManagement
{
    [UsedImplicitly]
    public class AssetProvider : IAssetProvider, IDisposable
    {
        private readonly List<AsyncOperationHandle> _operationHandles = new();
        
        public void LoadAssetWithCallback<TAsset>(AssetReference assetReference, Action<TAsset> callback)
        {
            var operation = assetReference.LoadAssetAsync<TAsset>();
            operation.Completed += (handler) =>
            {
                if (handler.IsDone)
                {
                    callback.Invoke(handler.Result);
                }
            };
        }

        public async Task<TAsset> LoadAssetAsync<TAsset>(string assetPath)
        {
            AsyncOperationHandle<TAsset> operation = Addressables.LoadAssetAsync<TAsset>(assetPath);
            await operation.Task;

            if (!_operationHandles.Contains(operation))
            {
                _operationHandles.Add(operation);
            }
            
            return operation.Result;
        }

        public void Dispose()
        {
            foreach (AsyncOperationHandle operationHandle in _operationHandles)
            {
                operationHandle.Release();
            }
            _operationHandles.Clear();
        }
    }
}