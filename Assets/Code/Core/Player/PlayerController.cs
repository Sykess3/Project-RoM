using Code.Utils;
using Mirror;
using RoM.Code.Core.Infrastructure.AppInitialization.Steps;
using RoM.Code.Core.Input;
using UnityEngine;

namespace RoM.Code.Core.Player
{
    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] private MasterInput _input;
        [SerializeField] private CharacterController _controller;
        [SerializeField] private Animator _animator;

        [SerializeField] private HorizontalMovement _horizontalMovement;
        [SerializeField] private CameraRotation _cameraRotation;
        [SerializeField] private VerticalMovement _verticalMovement;


        private void Awake()
        {
            _verticalMovement.Init(transform, _animator);
            _horizontalMovement.Init(Camera.main, transform, _animator);
        }
        
        private void Update()
        {
            Move();
        }

        private void LateUpdate()
        {
            _cameraRotation.Rotate(_input.Look);
        }

        private void Move()
        {
            if (!hasAuthority)
            {
                return;
            }
            Vector3 horizontalVelocity = _horizontalMovement.GetVelocity(_input.MoveDirection, isSprinting: _input.Sprint, _controller.velocity);   
            Vector3 verticalVelocity = _verticalMovement.GetVelocity(_input.Jump);

            _controller.Move(verticalVelocity + horizontalVelocity);
        }
    }
}