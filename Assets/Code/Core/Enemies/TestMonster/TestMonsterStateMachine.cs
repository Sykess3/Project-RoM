using System;
using RoM.Code.Core.Enemies.CommonStates;
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
            var patrol = new Patrol(collider, follower: _monster);
            var agentMoveToTarget = new AgentMoveToTarget(GetComponent<NavMeshAgent>(), _monster);
            AddTransition(patrol, agentMoveToTarget, HastTarget());
            SetState(patrol);
        }

        private Func<bool> HastTarget() => 
            () => _monster.Target != null;
    }
}