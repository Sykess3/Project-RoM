using UnityEngine;

namespace RoM.Code.Utils
{
    public class DontDestroy : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}