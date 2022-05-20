using UnityEngine;

namespace RoM.Code.Utils
{
    public static class EditorHelper
    {
        public static T GetComponent<T>(MonoBehaviour monoBehaviour, T field) where T : Component
        {
            if (field == null)
            {
                if (monoBehaviour.TryGetComponent(out T obj))
                {
                    return obj;
                }

                Debug.LogError($"Serialize fucking field of type {typeof(T).Name}");
            }

            return field;
        }
    }
}