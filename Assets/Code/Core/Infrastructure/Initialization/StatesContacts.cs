using System;
using System.Collections.Generic;

namespace RoM.Code.Core.Infrastructure.Initialization
{
    public interface IExitableState
    {
        void Exit();
    }

    public interface IState : IExitableState
    {
        void Enter();
    }

    public interface IPayloadedState : IExitableState
    {
        void Enter(InitializationServices payLoaded);
    }
    
    public class InitializationServices
    {
        public KeyValuePair<Type, object> ServicesToRegister;
    }
}