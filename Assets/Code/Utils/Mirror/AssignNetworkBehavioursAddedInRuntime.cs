using System;
using System.Collections.Generic;
using Mirror;
using Plugins.Mirror.Runtime.CustomCode;
using UnityEngine;

namespace RoM.Code.Utils.Mirror
{
    public class AssignNetworkBehavioursAddedInRuntime : MonoBehaviour
    {
        private IRuntimeAddableNetworkBehaviour[] _runtimeAddableNetworkBehaviours;

        private void Awake()
        {
            _runtimeAddableNetworkBehaviours = GetComponents<IRuntimeAddableNetworkBehaviour>();
            SubscribeOnLocalPlayerDispatch();
        }

        private void SubscribeOnLocalPlayerDispatch()
        {
            foreach (var runtimeAddableNetworkBehaviour in _runtimeAddableNetworkBehaviours)
            {
                runtimeAddableNetworkBehaviour.LocalPlayerAddedNetworkBehaviourComponents +=
                    AssignNetworkIdentityToNetworkBehaviours;
            }

            foreach (var runtimeAddableNetworkBehaviour in _runtimeAddableNetworkBehaviours)
            {
                runtimeAddableNetworkBehaviour.Destroyed += () => Destroy(this);
            }
        }

        private void UnsubscribeOnLocalPlayerDispatch()
        {
            foreach (var runtimeAddableNetworkBehaviour in _runtimeAddableNetworkBehaviours)
            {
                runtimeAddableNetworkBehaviour.LocalPlayerAddedNetworkBehaviourComponents -=
                    AssignNetworkIdentityToNetworkBehaviours;
            }
        }
        private void AssignNetworkIdentityToNetworkBehaviours(IEnumerable<NetworkBehaviour> obj)
        {
            var networkIdentity = GetComponent<NetworkIdentity>();
            foreach (var networkBehaviour in obj)
            {
                NetworkIdentityUtility.AssignIdentityToNetworkBehaviour(networkBehaviour, networkIdentity);
            }
            
            UnsubscribeOnLocalPlayerDispatch();
        }
    }
}