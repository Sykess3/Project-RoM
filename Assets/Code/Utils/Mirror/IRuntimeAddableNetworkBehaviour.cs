using System;
using System.Collections.Generic;
using Mirror;

namespace RoM.Code.Utils.Mirror
{
    public interface IRuntimeAddableNetworkBehaviour
    {
        /// <summary>
        /// Add [RequireComponent(typeof(AssignNetworkBehavioursAddedInRuntime))] to inherited component
        /// thanks Unity for good support to interfaces in monobehaviours <3
        /// </summary>
        event Action<IEnumerable<NetworkBehaviour>> LocalPlayerAddedNetworkBehaviourComponents;

        event Action Destroyed;
    }
}