using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Mirror;
using RoM.Code.Core.Input;
using RoM.Code.Core.Player;
using RoM.Code.Utils.Mirror;
using UnityEngine;
using VContainer;
using Object = UnityEngine.Object;

namespace RoM.Code.Core.Clients
{

    [RequireComponent(typeof(AssignNetworkBehavioursAddedInRuntime))]
    public class PlayerSetup : NetworkBehaviour, IRuntimeAddableNetworkBehaviour
    {
        [SerializeField] private GameObject _cinemachineCameraLookTarget;
        [SerializeField] private CameraSetup _cameraSetup;

        [Inject] private Camera _camera;

        public event Action<IEnumerable<NetworkBehaviour>> LocalPlayerAddedNetworkBehaviourComponents;
        public event Action Destroyed;

        public override void OnStartAuthority()
        {
            print(nameof(OnStartAuthority));
        }

        public override void OnStartClient()
        {
            print(nameof(OnStartClient));
            if (!hasAuthority)
            {
                gameObject.name = "RemotePlayer";
                Destroy(this);
            }
            else
            {
                gameObject.name = "LocalPlayer";
            }
        }

        public override void OnStartServer()
        {
            if (!isClient)
            {
                InitializeServerPlayer();
            }
        }

        private void InitializeServerPlayer()
        {
            ConstructPlayer();
        }

        public override void OnStartLocalPlayer()
        {
            print(nameof(OnStartLocalPlayer));
            InitializeLocalPlayer();
        }

        private void InitializeLocalPlayer()
        {
            var masterInput = gameObject.AddComponent<MasterInput>();
            var playerCharacterFacade = ConstructPlayer();
            playerCharacterFacade.Init(masterInput, GetComponent<Animator>(), GetComponent<CharacterController>());

            _cameraSetup.Setup(transform);
            
            Destroy(this);
        }

        private PlayerCharacterFacade ConstructPlayer()
        {
            var playerCharacterFacade = gameObject.AddComponent<PlayerCharacterFacade>();
            playerCharacterFacade.CameraRotation = new CameraRotation(_cinemachineCameraLookTarget);
            playerCharacterFacade.HorizontalMovement =
                new HorizontalMovement(Camera.main, transform, GetComponent<Animator>());
            playerCharacterFacade.VerticalMovement =
                new VerticalMovement(transform, GetComponent<Animator>());

            var addedNetworkBehaviours = new NetworkBehaviour[]
            {
                playerCharacterFacade
            };
            LocalPlayerAddedNetworkBehaviourComponents?.Invoke(addedNetworkBehaviours);

            return playerCharacterFacade;
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke();
        }
    }

    [Serializable]
    class CameraSetup
    {
        public PlayerCharacterForwardingCamera ForwardingCamera;
        public GameObject FollowObject;

        public void Setup(Transform playerCharacterRoot)
        {
            var camera = Object.Instantiate(ForwardingCamera, playerCharacterRoot)
                .GetComponent<CinemachineVirtualCamera>();
            camera.Follow = FollowObject.transform;
        }
    }
    
}