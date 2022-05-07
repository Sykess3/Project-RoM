using System;
using Mirror;
using RoM.Code.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RoM.Code.Core.Input
{
    public class MasterInput : NetworkBehaviour
    {
        [field: SerializeField] public Vector2 MoveDirection { get; private set; }
        [field: SerializeField] public Vector2 Look { get; private set; }
        [field: SerializeField] public bool Sprint { get; private set; }
        [field: SerializeField] public bool Jump { get; private set; }

        [field: SerializeField] public bool IsControlDisabled { get; private set; }

        protected CommonInput _playerInput;


        private void Awake()
        {
            _playerInput = new CommonInput();
            _playerInput.Enable();

            RegisterEvents();
        }

        public override void OnStartClient()
        {
            if (!hasAuthority)
            {
                _playerInput.Disable();
            }
        }

        private void RegisterEvents()
        {
            _playerInput.Player.Move.performed += OnMove;
            _playerInput.Player.Jump.performed += OnJump;
            _playerInput.Player.Look.performed += OnLook;
            _playerInput.Player.Sprint.performed += OnSprint;

            _playerInput.Player.Move.canceled += OnMoveCanceled;
            _playerInput.Player.Look.canceled += OnLookCanceled;
            _playerInput.Player.Sprint.canceled += OnSprintCanceled;
            _playerInput.Player.Jump.canceled += OnJumpCanceled;
        }


        public void Enable() => IsControlDisabled = false;

        public void Disable() => IsControlDisabled = true;
        private void OnJumpCanceled(InputAction.CallbackContext obj) => Jump = false;

        private void OnLookCanceled(InputAction.CallbackContext obj) => Look = Vector2.zero;

        private void OnSprintCanceled(InputAction.CallbackContext obj) => Sprint = false;

        private void OnMoveCanceled(InputAction.CallbackContext obj) => MoveDirection = Vector2.zero;

        private void OnMove(InputAction.CallbackContext context) => MoveDirection = context.ReadValue<Vector2>();

        private void OnLook(InputAction.CallbackContext context) => Look = context.ReadValue<Vector2>();

        private void OnSprint(InputAction.CallbackContext context) => Sprint = true;

        private void OnJump(InputAction.CallbackContext context) => Jump = true;
    }
}