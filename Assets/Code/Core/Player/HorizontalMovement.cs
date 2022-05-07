using UnityEngine;

namespace RoM.Code.Core.Player
{
	[System.Serializable]
    public class HorizontalMovement
    {
	    [Tooltip("Move speed of the character in m/s")]
        [SerializeField] private float MoveSpeed = 2.0f;
        
        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        [SerializeField] private float RotationSmoothTime = 0.12f;
        
        [Tooltip("Sprint speed of the character in m/s")]
        [SerializeField] private float SprintSpeed = 5.335f;
        
        [Tooltip("Acceleration and deceleration")]
        [SerializeField] private float SpeedChangeRate = 10.0f;
        
        private int _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        private int _animIDSpeed = Animator.StringToHash("Speed");
        
        private Camera _mainCamera;
        private Transform _transform;
        private Animator _animator;
        
        private float _speed;
        private float _rotationVelocity;
        private float _targetRotation;
        private float _animationBlend;

        public void Init(Camera camera, Transform transform, Animator animator)
        {
	        _mainCamera = camera;
	        _transform = transform;
	        _animator = animator;
        }

        public Vector3 GetVelocity(Vector2 moveDirection, bool isSprinting, Vector3 currentVelocity)
		{
			float targetSpeed = isSprinting ? SprintSpeed : MoveSpeed;
			
			if (moveDirection == Vector2.zero) 
				targetSpeed = 0.0f;
			
			float currentHorizontalSpeed = new Vector3(currentVelocity.x, 0, currentVelocity.z).magnitude;

			float speedOffset = 0.1f;
			float inputMagnitude = moveDirection.magnitude;
			
			if (IsMaximalSpeed(currentHorizontalSpeed, targetSpeed, speedOffset))
			{
				_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);
				
				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else
			{
				_speed = targetSpeed;
			}
			
			_animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
			if (_animationBlend < 0.01f) 
				_animationBlend = 0f;
			
			Vector3 inputDirection = new Vector3(moveDirection.x, 0.0f, moveDirection.y).normalized;

			if (moveDirection != Vector2.zero)
			{
				_targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
				float rotation = Mathf.SmoothDampAngle(_transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);
				
				_transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
			}


			Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
			
			_animator.SetFloat(_animIDSpeed, _animationBlend);
			_animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
			
			return targetDirection.normalized * _speed * Time.deltaTime;

		}

        private static bool IsMaximalSpeed(float currentHorizontalSpeed, float targetSpeed, float speedOffset)
        {
	        return currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset;
        }
    }
}