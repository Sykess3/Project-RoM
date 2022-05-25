using System;
using RoM.Code.Core.Infrastructure;
using RoM.Code.Core.Infrastructure.Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace RoM.Code.UI
{
    public class StartMultiplayerPage : MonoBehaviour
    {
        [SerializeField] private Button _server;
        [SerializeField] private Button _client;
        [SerializeField] private Button _host;
        [SerializeField] private RoMNetworkManager _network;
        [SerializeField] private RootLifetime _container;
        

        private void Start()
        {
#if UNITY_EDITOR
            return;
#endif
            
#if UNITY_SERVER
            OnServerClick();
#else
            OnClientClick();
#endif
        }

        private void OnEnable()
        {
            _server.onClick.AddListener(OnServerClick);
            _client.onClick.AddListener(OnClientClick);
            _host.onClick.AddListener(OnHostClick);
        }

        private void OnClientClick()
        {
            _network.StartClient();
            print("======CLIENT CONNECTED======");
            gameObject.SetActive(false);
            _container.Build();
        }

        private void OnHostClick()
        {
            _network.StartHost();
            print("======HOST STARTED======");
            gameObject.SetActive(false);
            _container.Build();
        }

        private void OnServerClick()
        {
            _network.StartServer();
            print("======SERVER STARTED======");
            gameObject.SetActive(false);
            _container.Build();
        }
    }
}