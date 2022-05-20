using Mirror;

namespace Plugins.Mirror.Runtime.CustomCode
{
    public struct NetworkIdentityUtility
    {
        public static void AssignIdentityToNetworkBehaviour(NetworkBehaviour networkBehaviour, NetworkIdentity networkIdentity)
        {
            networkBehaviour.netIdentity = networkIdentity;
        }
    }
}