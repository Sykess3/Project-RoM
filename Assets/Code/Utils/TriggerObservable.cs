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
    }
}