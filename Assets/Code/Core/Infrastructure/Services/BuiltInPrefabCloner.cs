using System;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RoM.Code.Core.Infrastructure.Services
{
    public class BuiltInPrefabCloner : IAssetProvider
    {
        public async Task<T> InstantiateAsync<T>(string path) where T : Object
        {
            var prefab = Resources.Load<T>(path);
            await Task.Delay(10);
            return Object.Instantiate(prefab);
        }

        public async Task<T> InstantiateAsync<T>(string path, Vector3 position, Quaternion rotation) where T : Object
        {
            var prefab = Resources.Load<T>(path);
            await Task.Delay(10);
            return Object.Instantiate(prefab, position, rotation);
        }

        public async Task<T> InstantiateAsync<T>(string path, Transform under) where T : Object
        {
            var prefab = Resources.Load<T>(path);
            await Task.Delay(10);
            return Object.Instantiate(prefab, under);
        }
    }
}