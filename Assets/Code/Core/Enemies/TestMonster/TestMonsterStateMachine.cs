using System;
using RoM.Code.Core.Enemies.CommonStates;
using RoM.Code.Core.Enemies.CommonStates.Transitions;
using RoM.Code.Core.Enemy;
using RoM.Code.Core.Enemy.CommonStates;
using RoM.Code.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace RoM.Code.Core.Enemies.TestMonster
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class TestMonsterStateMachine : NPCStateMachine
    {
        [SerializeField] private FieldOfViewCheck _fov;
        private Monster _monster;
        

        private void Start()
        {
            _monster = GetComponent<Monster>();
            var collider = GetComponentInChildren<TriggerObservable>();
            
            _fov.Init(transform, _monster);
            
            var aggro = new Aggro(collider, follower: _monster);
            var agentMoveToTarget =
                new AgentMoveToTarget(GetComponent<NavMeshAgent>(), _monster);

            AddTransition(aggro, agentMoveToTarget, HasTarget(), _fov.CanTransit);
            AddTransition(agentMoveToTarget, aggro,
                new TriggerTransition.OnExit(collider, IsSameTarget(), delayTime: 1));

            SetState(aggro);
        }

        private Func<Collider, bool> IsSameTarget() =>
            (other) => _monster.Target == other.transform;

        private Func<bool> HasNotTarget() =>
            () => _monster.Target == null;

        private Func<bool> HasTarget() =>
            () => _monster.Target != null;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            _fov.SetTransform(transform);
            _fov.DrawGizmos();
        }
#endif
    }
}