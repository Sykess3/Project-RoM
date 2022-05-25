using System;
using System.Collections.Generic;

namespace RoM.Code.Core.Infrastructure.Initialization
{
    public class StateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState>(InitializationServices payload) where TState :
            class, IPayloadedState
        {
            IPayloadedState state = ChangeState<TState>();
            state.Enter(payload);
        }

        public TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();
            TState state = GetState<TState>();
            _currentState = state;
            return state;
        }

        public TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}