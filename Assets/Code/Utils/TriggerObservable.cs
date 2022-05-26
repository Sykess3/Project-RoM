using System;
using UnityEngine;

namespace RoM.Code.Utils
{
    public class TriggerObservable : MonoBehaviour
    {
        public event Action<Collider> Entered;
        public event Action<Collider> Exited;

        private void OnTriggerEnter(Collider other)
        {
            Entered?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            Exited?.Invoke(other);
        }

#if UNITY_EDITOR
        private SphereCollider _sphereCollider;

        private void OnDrawGizmos()
        {
            if (_sphereCollider == null)
            {
                var trigger = GetComponentInChildren<TriggerObservable>();
                if (trigger == null)
                    return;

                _sphereCollider = trigger.GetComponent<SphereCollider>();
                if (_sphereCollider == null)
                    return;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_sphereCollider.transform.position + _sphereCollider.center, _sphereCollider.radius);
            Gizmos.color = Color.white;
        }
    }
#endif
}