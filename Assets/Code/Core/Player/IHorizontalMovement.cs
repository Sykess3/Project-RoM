using UnityEngine;

namespace RoM.Code.Core.Player
{
    public interface IHorizontalMovement
    {
        Vector3 GetVelocity(Vector2 moveDirection, bool isSprinting, Vector3 currentVelocity);
    }
}