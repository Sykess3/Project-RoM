using System;
using Mirror;
using RoM.Code.Core.Input;
using RoM.Code.Core.NPCs.Enemies.StateMachineAbstractions;
using UnityEngine;
using EditorHelper = RoM.Code.Utils.EditorHelper;

namespace RoM.Code.Core.Player
{
    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public class PlayerCharacterFacade : NetworkBehaviour, IDamageable
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private Animator _animator;

        private MasterInput _input;
        public IHorizontalMovement HorizontalMovement { get; set; }
        public ICameraRotation CameraRotation { get; set; }
        public IVerticalMovement VerticalMovement { get; set; }

        public void Init(MasterInput input, Animator animator, CharacterController controller)
        {
            _input = input;
            _animator = animator;
            _controller = controller;
        }

#if !UNITY_SERVER
        private void Update()
        {
            Move();
        }

        private void LateUpdate()
        {
            CameraRotation.Rotate(_input.Look);
        }
#endif
        private void Move()
        {
            if (!hasAuthority)
            {
                return;
            }

            Vector3 horizontalVelocity = HorizontalMovement.GetVelocity(_input.MoveDirection,
                isSprinting: _input.Sprint, _controller.velocity);
            Vector3 verticalVelocity = VerticalMovement.GetVelocity(_input.Jump);

            _controller.Move(verticalVelocity + horizontalVelocity);
        }

        public void TakeDamage(float amount)
        {
            Debug.Log(amount);
        }
    }
}