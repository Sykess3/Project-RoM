using System;
using RoM.Code.Core.NPCs.Enemies.CommonStates;
using RoM.Code.Core.NPCs.Enemies.CommonStates.Transitions;
using RoM.Code.Core.NPCs.StateMachine;
using RoM.Code.UI;
using RoM.Code.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace RoM.Code.Core.NPCs.Enemies.TestEnemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class TestEnemyStateMachine : NPCStateMachine
    {
        [SerializeField] private FieldOfViewCheck _fov;
        [SerializeField] private Attack _attack;

        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private Enemy _enemy;
        private NavMeshAgent _agent;

        private void Start()
        {
            _enemy = GetComponent<Enemy>();
            var collider = GetComponentInChildren<TriggerObservable>();
            _agent = GetComponent<NavMeshAgent>();

            _fov.Init(transform, _enemy);

            var aggro = new Aggro(collider, follower: _enemy);
            var agentMoveToTarget =
                new AgentMoveToTarget(_agent, _enemy, _animator, GetComponent<AgentLinkMover>());
            _attack.Init(
                transform,
                attacker: _enemy,
                follower: _enemy,
                animator: _animator);

            _animator.OnAttackPerformCallback = _attack.OnAttackPerform_AnimationEvent;
            _animator.OnAttackEndedCallback = _attack.OnAttackEnded_AnimationEvent;

            AddTransition(aggro, agentMoveToTarget, HasTarget(), _fov.CanTransit);
            AddTransition(agentMoveToTarget, aggro, new IsPlayerInAggroRange(collider, _enemy, 2));
            AddTransition(agentMoveToTarget, _attack, IsStoppingDistanceReached());
            AddTransition(_attack, agentMoveToTarget, IsRanOutOfStoppingDistanceAndNotAttacking());
            
            SetState(aggro);
        }
        

        private Func<bool> HasTarget() =>
            () => _enemy.Target != null;

        private Func<bool> IsRanOutOfStoppingDistanceAndNotAttacking() =>
            () =>  Vector3.Distance(_agent.transform.position, _enemy.Target.transform.position) > _agent.stoppingDistance + 0.1f && !_attack.isAttacking;

        private Func<bool> IsStoppingDistanceReached() =>
            () => _agent.remainingDistance <= _agent.stoppingDistance;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            _fov.SetTransform(transform);
            _fov.DrawGizmos();
            
            _attack.SetTransform(transform);
            _attack.OnDrawGizmos();
        }
#endif
    }
}