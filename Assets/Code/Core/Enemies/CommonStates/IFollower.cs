using UnityEngine;

namespace RoM.Code.Core.Enemy.CommonStates
{
    public interface IFollower
    {
        Transform Target { get; set; }
    }
}