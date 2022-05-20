using System.Threading.Tasks;
using UnityEngine;

namespace RoM.Code.Core.Infrastructure.Services
{
    public interface IAssetProvider
    {
        Task<T> InstantiateAsync<T>(string path) where T : Object;
        Task<T> InstantiateAsync<T>(string path, Vector3 position, Quaternion rotation) where T : Object;
        Task<T> InstantiateAsync<T>(string path, Transform under) where T : Object;
    }
}