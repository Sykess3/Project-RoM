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
        private Monster _monster;

        private void Start()
        {
            _monster = GetComponent<Monster>();
            var collider = GetComponentInChildren<TriggerObservable>();

            var aggro = new Aggro(collider, follower: _monster);
            var agentMoveToTarget =
                new AgentMoveToTarget(GetComponent<NavMeshAgent>(), _monster);

            AddTransition(aggro, agentMoveToTarget, HasTarget());
            AddTransition(agentMoveToTarget, aggro,
                new TriggerTransition.OnExit(collider, IsSameTarget(), additionTime: 1));

            SetState(aggro);
        }

        private Func<Collider, bool> IsSameTarget()
        {
            return (other) => _monster.Target == other.transform;
        }


        private Func<bool> HasNotTarget() =>
            () => _monster.Target == null;


        private Func<bool> HasTarget() =>
            () => _monster.Target != null;
    }
}