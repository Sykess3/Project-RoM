using UnityEngine;

namespace RoM.Code.Core.Player
{
    public class VecticalMovement_LocalPlayer : IVerticalMovement
    {
        [Tooltip("Useful for rough ground")] 
        private float _groundedOffset = -0.14f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        private float _groundedRadius = 0.28f;

        [Tooltip("What layers the character uses as ground")] 
        private LayerMask _groundLayers;

        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        private float _jumpTimeout = 0.50f;

        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        private float _fallTimeout = 0.15f;

        [Tooltip("The height the player can jump")] 
        private float _jumpHeight = 1.2f;

        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")] 
        private float _gravity = -15.0f;

        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        private Transform _transform;
        private Animator _animator;
        
        private int _animIDJump = Animator.StringToHash("Jump");
        private int _animIDFreeFall = Animator.StringToHash("FreeFall");
        private int _animIDGrounded = Animator.StringToHash("Grounded");

        public bool Grounded { get; private set; }
        public bool IsInJump { get; private set; }

        public VecticalMovement_LocalPlayer(Transform transform, Animator animator)
        {
            _transform = transform;
            _animator = animator;
            _groundLayers = LayerMask.GetMask("Default");
        }

        public Vector3 GetVelocity(bool isJumping)
        {
            Grounded = IsGrounded();
            if (Grounded)
            {
                _fallTimeoutDelta = _fallTimeout;
                
                _animator.SetBool(_animIDJump, false);
                _animator.SetBool(_animIDFreeFall, false);

                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }
                
                if (isJumping && _jumpTimeoutDelta <= 0.0f)
                {
                    _verticalVelocity = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
                    _animator.SetBool(_animIDJump, true);
                }
                
                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                _jumpTimeoutDelta = _jumpTimeout;

                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    _animator.SetBool(_animIDFreeFall, true);
                }
            }
            
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += _gravity * Time.deltaTime;
            }

            return new Vector3(0, _verticalVelocity * Time.deltaTime, 0);
        }
        
        private bool IsGrounded()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(_transform.position.x, _transform.position.y - _groundedOffset,
                _transform.position.z);
            bool isGrounded = Physics.CheckSphere(spherePosition, _groundedRadius, _groundLayers,
                QueryTriggerInteraction.Ignore);
            _animator.SetBool(_animIDGrounded, Grounded);
            return isGrounded;
        }
    }
}