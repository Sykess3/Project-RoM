using RoM.Code.Core.Enemy;
using RoM.Code.Core.Enemy.CommonStates;
using RoM.Code.Core.Player;
using RoM.Code.Utils;
using UnityEngine;

namespace RoM.Code.Core.Enemies.CommonStates
{
    public class Patrol : INPCState
    {
        private readonly TriggerObservable _collider;
        private readonly IFollower _follower;

        public Patrol(TriggerObservable collider, IFollower follower)
        {
            _collider = collider;
            _follower = follower;
        }

        public void Tick()
        {
            if (_follower.Target == null)
                return;
            
            // add field of view check
        }

        public void OnExit()
        {
            _collider.Entered -= TrySetTarget;
        }

        public void OnEnter()
        {
            _collider.Entered += TrySetTarget;
        }

        private void TrySetTarget(Collider obj)
        {
            // TODO: Add physics layer check
            if (obj.TryGetComponent(out PlayerCharacterFacade character))
            {
                _follower.Target = character.transform;   
            }
        }
    }
}