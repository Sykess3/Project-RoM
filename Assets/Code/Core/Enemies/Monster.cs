using System;
using RoM.Code.Core.Enemy.CommonStates;
using RoM.Code.Utils;
using UnityEngine;

namespace RoM.Code.Core.Enemy
{
    public class Monster : MonoBehaviour, IFollower
    {
        public Transform Target { get; set; }
    }
}