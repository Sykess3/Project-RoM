using RoM.Code.Core.Enemy;
using RoM.Code.Core.Enemy.CommonStates;
using UnityEngine.AI;

namespace RoM.Code.Core.Enemies.CommonStates
{
    public class AgentMoveToTarget : INPCState
    {
        private readonly NavMeshAgent _agent;
        private readonly IFollower _follower;

        public AgentMoveToTarget(NavMeshAgent agent, IFollower follower)
        {
            _agent = agent;
            _follower = follower;
        }
        
        public void Tick()
        {
            _agent.SetDestination(_follower.Target.position);
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}