using RoM.Code.Core.NPCs.Enemies.StateMachineAbstractions;
using RoM.Code.Core.NPCs.Enemies.TestEnemy;
using RoM.Code.Core.NPCs.StateMachine;
using RoM.Code.UI;
using UnityEngine;
using UnityEngine.AI;

namespace RoM.Code.Core.NPCs.Enemies.CommonStates
{
    public class AgentMoveToTarget : INPCState
    {
        private readonly NavMeshAgent _agent;
        private readonly IFollower _follower;
        private readonly EnemyAnimator _animator;
        private readonly AgentLinkMover _agentLinkMover;
        
        private static readonly int DoJumpWhileRunning = Animator.StringToHash("DoJumpWhileRunning");
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");

        public AgentMoveToTarget(NavMeshAgent agent,
            IFollower follower,
            EnemyAnimator animator,
            AgentLinkMover agentLinkMover)
        { _agent = agent;
            _follower = follower;
            _animator = animator;
            _agentLinkMover = agentLinkMover;
        }

        public void Tick()
        {
            _agent.SetDestination(_follower.Target.position);
        }

        public void OnEnter()
        {
            _agentLinkMover.Jumping += AnimateJump;
            _animator.StartRunning();
        }

        private void AnimateJump()
        {
            _animator.JumpWhileRunning();   
        }

        public void OnExit()
        {
            _agent.SetDestination(_agent.transform.position);
            _animator.StopRunning();
        }
    }
}