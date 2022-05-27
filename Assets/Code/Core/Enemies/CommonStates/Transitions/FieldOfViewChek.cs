using RoM.Code.Core.Enemy.CommonStates;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Timer = Code.Utils.Timer;

namespace RoM.Code.Core.Enemies.CommonStates.Transitions
{
    [System.Serializable]
    public class FieldOfViewCheck : INPCTransition
    {
        private const float SavePerformanceDelay = 0.4f;

        [SerializeField] private float _angle;
        [SerializeField] private LayerMask _playerMask;
        private Transform _transform;
        private IFollower _follower;
        private Timer _timer;

        private bool _canSeePlayer;

        public void Init(Transform transform, IFollower follower)
        {
            _transform = transform;
            _follower = follower;

            _timer = new Timer();
            _timer.Start(SavePerformanceDelay);
        }

        public bool CanTransit()
        {
            if (!_timer.IsReached)
                return false;

            _timer.Start(SavePerformanceDelay);
            return IsPlayerInFoV();
        }

        private bool IsPlayerInFoV()
        {
            // if (_follower.Target == null)
            // {
            //     return false;
            // }

            Vector2 directionToTarget = new Vector2(_follower.Target.position.x, _follower.Target.position.z) -
                                        new Vector2(_transform.position.x, _transform.position.z);
            var forwardVector2 = new Vector2(_transform.forward.x, _transform.forward.z);
            
            if (Vector2.Angle(forwardVector2, directionToTarget.normalized) < _angle / 2)
            {
                var distanceToTarget = directionToTarget.magnitude;
                _canSeePlayer = Physics.Raycast(_transform.position, _follower.Target.position - _transform.position, distanceToTarget + 1,
                    _playerMask);
                return _canSeePlayer;
            }

            return _canSeePlayer = false;
        }
#if UNITY_EDITOR


        public void SetTransform(Transform transform)
        {
            _transform = transform;
        }

        public void DrawGizmos()
        {
            Vector3 viewAngle01 = DirectionFromAngle(_transform.eulerAngles.y, -_angle / 2);
            Vector3 viewAngle02 = DirectionFromAngle(_transform.eulerAngles.y, _angle / 2);

            Gizmos.color = Color.yellow;
            var sphereCollider = _transform.GetComponentInChildren<SphereCollider>();
            Gizmos.DrawLine(_transform.position, _transform.position + viewAngle01 * sphereCollider.radius);
            Gizmos.DrawLine(_transform.position, _transform.position + viewAngle02 * sphereCollider.radius);

            if (_canSeePlayer && _follower.Target != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(_transform.position, _transform.position - _follower.Target.position);
                Gizmos.color = Color.white;
            }

            Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
            {
                angleInDegrees += eulerY;

                return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0,
                    Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
            }
        }
#endif
    }
}