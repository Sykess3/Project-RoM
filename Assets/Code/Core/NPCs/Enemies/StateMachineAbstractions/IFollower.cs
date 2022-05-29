using UnityEngine;

namespace RoM.Code.Core.NPCs.Enemies.StateMachineAbstractions
{
    public interface IFollower
    {
        Transform Target { get; set; }
    }
}