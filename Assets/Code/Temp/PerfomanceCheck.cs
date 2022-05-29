using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace RoM.Code.UI
{
    public class PerfomanceCheck : MonoBehaviour
    {
        private Collider[] _collider = new Collider[0];

        private void Update()
        {
            Capsule();
            Sphere();
            Box();
        }

        public void Capsule()
        {
            for (int i = 0; i < 1000; i++)
            {
                Physics.OverlapCapsuleNonAlloc(transform.position, transform.position + Vector3.back * 2, 2, _collider);
            }
        }

        private void Sphere()
        {
            for (int i = 0; i < 1000; i++)
            {
                Physics.OverlapSphereNonAlloc(transform.position, 2, _collider);
                Physics.OverlapSphereNonAlloc(transform.position, 2, _collider);
            }
        }

        private void Box()
        {
            for (int i = 0; i < 1000; i++)
            {
                Physics.OverlapBoxNonAlloc(transform.position, Vector3.one * 2, _collider);
            }
        }
    }
}