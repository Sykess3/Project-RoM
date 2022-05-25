using System;
using RoM.Code.Core.Enemy.CommonStates;
using RoM.Code.Utils;
using UnityEngine;

namespace RoM.Code.Core.Enemy
{
    public class Monster : MonoBehaviour, IFollower
    {
        public Transform Target { get; set; }

#if UNITY_EDITOR
        private SphereCollider _sphereCollider;
#endif
        
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
}