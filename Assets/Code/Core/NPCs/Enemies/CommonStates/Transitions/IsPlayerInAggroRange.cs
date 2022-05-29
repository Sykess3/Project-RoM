using System;
using RoM.Code.Core.NPCs.Enemies.StateMachineAbstractions;
using RoM.Code.Core.NPCs.StateMachine;
using RoM.Code.Utils;
using UnityEngine;

namespace RoM.Code.Core.NPCs.Enemies.CommonStates.Transitions
{
    public class IsPlayerInAggroRange : INPCTransition, IDisposable
    {
        private readonly IFollower _follower;
        private readonly TriggerTransition.OnExit _onExit;

        public IsPlayerInAggroRange(TriggerObservable triggerObservable, IFollower follower, float delayTime)
        {
            _follower = follower;
            _onExit = new TriggerTransition.OnExit(triggerObservable, IsSameTarget(), delayTime);
        }
        
        private Func<Collider, bool> IsSameTarget() =>
            (other) => _follower.Target == other.transform;

        public bool CanTransit()
        {
            var canTransit = _onExit.CanTransit();
            if (canTransit) 
                _follower.Target = null;

            return canTransit;
        }

        public void Dispose()
        {
            _onExit.Dispose();
        }
    }
}