using System;
using UnityEngine;

namespace RoM.Code.Core.Infrastructure.Initialization
{
    public class GameBootstrapper : MonoBehaviour
    {
        private StateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = new StateMachine();
            
        }
    }
}