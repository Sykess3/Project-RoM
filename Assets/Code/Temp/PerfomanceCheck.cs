using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace RoM.Code.UI
{
    public class PerfomanceCheck : MonoBehaviour
    {
        public Transform Transform;
        private Transform _transform;

        private void Update()
        {
            DotProduct();
            AngleMethod();
        }

        private void AngleMethod()
        {
            var normalized = (Transform.position - _transform.position);
            Debug.Log(Vector3.Angle(_transform.forward, normalized));
        }

        private void DotProduct()
        {
            _transform = transform;
            var rot = _transform.forward;
            var vector2 = new Vector2(rot.x, rot.z);
            var vector3 = new Vector2(Transform.position.x, Transform.position.z);
            //Debug.Log(transform.forward);

            var dot = Vector2.Dot(vector2.normalized, vector3.normalized);
            //Debug.Log(Mathf.Acos(dot) * Mathf.Rad2Deg);
        }
    }
}