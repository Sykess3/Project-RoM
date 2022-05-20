using UnityEngine;

namespace RoM.Code.Core.Player
{
    public interface IVerticalMovement
    {
        bool Grounded { get; }
        bool IsInJump { get; }
        Vector3 GetVelocity(bool isJumping);
    }
}