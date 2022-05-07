using UnityEngine;

namespace RoM.Code.Core.Player
{
    [System.Serializable]
    public class CameraRotation
    {
        private const float Threshold = 0.01f;
        
        [Tooltip("How far in degrees can you move the camera up")]
        [SerializeField] private float _topClamp = 70.0f;
        [Tooltip("How far in degrees can you move the camera down")]
        [SerializeField] private float _bottomClamp = -30.0f;
        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        [SerializeField] private float _cameraAngleOverride;
        
        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        [SerializeField] private GameObject _cinemachineCameraTarget;
        
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        
        public void Rotate(Vector2 lookRotation)
        {
            if (lookRotation.sqrMagnitude >= Threshold)
            {
                _cinemachineTargetYaw += lookRotation.x;
                _cinemachineTargetPitch += lookRotation.y;
            }
            
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, _bottomClamp, _topClamp);
            
            _cinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + _cameraAngleOverride, _cinemachineTargetYaw, 0.0f);
        }
        
        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}