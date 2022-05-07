using Mirror;
using RoM.Code.Core.Player;
using UnityEngine;

namespace RoM.Code.Core.Clients
{
    public class PlayerInstance : NetworkBehaviour
    {
        //TODO: Remove fucking singletone u fucking bullshit
        public static PlayerInstance Instance;

        [SerializeField] private PlayerController _playerPrefab;

        public override void OnStartServer()
        {
            base.OnStartServer();

            NetworkSpawnPlayer();
        }

        [Server]
        private void NetworkSpawnPlayer()
        {
            GameObject go = Instantiate(_playerPrefab.gameObject);
            NetworkServer.Spawn(go, connectionToClient);
        }
    }
}