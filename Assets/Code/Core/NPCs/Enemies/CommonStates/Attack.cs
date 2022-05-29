using Code.Utils;
using RoM.Code.Core.NPCs.Enemies.StateMachineAbstractions;
using RoM.Code.Core.NPCs.Enemies.TestEnemy;
using RoM.Code.Core.NPCs.StateMachine;
using RoM.Code.Core.Player;
using UnityEngine;

namespace RoM.Code.Core.NPCs.Enemies.CommonStates
{
    [System.Serializable]
    public class Attack : INPCState
    {
        [SerializeField] private float _radius;
        [SerializeField] private Vector3[] _attackPositions;
        [SerializeField] private LayerMask _mask;

        private IAttackable _attacker;
        private IFollower _follower;
        private EnemyAnimator _animator;
        private Transform _transform;

        private float _delayBeforeNextAttack;
        private Timer _timer;

        private Collider[] _hit = new Collider[1];

        public bool isAttacking { get; private set; }

        public void Init(Transform transform,
            IAttackable attacker,
            IFollower follower,
            EnemyAnimator animator,
            float delayBeforeNextAttack = 1.5f)
        {
            _attacker = attacker;
            _delayBeforeNextAttack = delayBeforeNextAttack;
            _animator = animator;
            _follower = follower;
            _transform = transform;
            _timer = new Timer();
            _timer.Start(0);
        }

        public void Tick()
        {
            if (isAttacking)
                return;

            if (_timer.IsReached)
            {
                StartAttackAnimation();
            }
            else
            {
                RotateTowardsTarget();
            }
        }

        private void StartAttackAnimation()
        {
            _animator.LeftPunch();
            isAttacking = true;
        }

        private void RotateTowardsTarget()
        {
            var targetRotation = Quaternion.LookRotation(_follower.Target.position - _transform.position);
            _transform.rotation = Quaternion.Lerp(_transform.rotation, targetRotation, 10 * Time.deltaTime);
        }

        public void OnAttackPerform_AnimationEvent()
        {
            _timer.Start(_delayBeforeNextAttack);

            if (OverlapSphere(out var damageable))
            {
                _attacker.Attack(damageable);
            }
        }


        //TODO: Remove sphere , overlap box is better for performance than overlap sphere
        private bool OverlapSphere(out PlayerCharacterFacade damageable)
        {
            damageable = null;

            for (int i = 0; i < _attackPositions.Length; i++)
            {
                Physics.OverlapSphereNonAlloc(AttackPosition(i), _radius, _hit, _mask, QueryTriggerInteraction.Ignore);
                if (_hit[0] != null && _hit[0].transform.root.TryGetComponent(out damageable))
                    return true;
            }

            return false;
        }

        public void OnAttackEnded_AnimationEvent()
        {
            isAttacking = false;
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }

        public Vector3 AttackPosition(int index) =>
            new Vector3(_transform.position.x, _transform.position.y + _attackPositions[index].y,
                _transform.position.z) +
            _transform.forward * _attackPositions[index].z;

#if UNITY_EDITOR

        public void SetTransform(Transform transform)
        {
            _transform = transform;
        }

        public void OnDrawGizmos()
        {
            if (isAttacking)
            {
                Gizmos.color = Color.red;
                for (int i = 0; i < _attackPositions.Length; i++)
                {
                    Gizmos.DrawWireSphere(AttackPosition(i), _radius);
                }

                Gizmos.color = Color.white;
            }

            if (!Application.isPlaying)
            {
                Gizmos.color = Color.red;
                for (int i = 0; i < _attackPositions.Length; i++)
                {
                    Gizmos.DrawWireSphere(AttackPosition(i), _radius);
                }

                Gizmos.color = Color.white;
            }
        }

#endif
    }
}