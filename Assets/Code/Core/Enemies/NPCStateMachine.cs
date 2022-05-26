using System;
using System.Collections.Generic;
using RoM.Code.Core.Enemies;
using UnityEngine;
using UnityEngine.AI;

namespace RoM.Code.Core.Enemy
{
    public class NPCStateMachine : MonoBehaviour
    {
        private INPCState _currentState;

        private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        private List<Transition> _currentTransitions = new List<Transition>();
        private List<Transition> _anyTransitions = new List<Transition>();
        private HashSet<IDisposable> _toCleanup = new HashSet<IDisposable>();

        private static List<Transition> EmptyTransitions = new List<Transition>(0);

        public void LateUpdate()
        {
            var transition = GetTransition();
            if (transition != null)
                SetState(transition.To);

            _currentState?.Tick();
        }

        private void OnDestroy()
        {
            foreach (var disposable in _toCleanup)
            {
                disposable.Dispose();
            }
        }

        public void SetState(INPCState state)
        {
            if (state == _currentState)
                return;

            _currentState?.OnExit();
            _currentState = state;

            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
            if (_currentTransitions == null)
                _currentTransitions = EmptyTransitions;

            _currentState.OnEnter();
        }

        public void AddTransition(INPCState from, INPCState to, Func<bool> predicate)
        {
            if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }

            transitions.Add(new Transition(to, predicate));
        }

        public void AddTransition(INPCState from, INPCState to, INPCTransition transition)
        {
            if (transition is IDisposable disposable) 
                _toCleanup.Add(disposable);
            
            AddTransition(from, to, transition.CanTransit);
        }
        /// <summary>
        /// All transitions passed in argument must be true in the same time
        /// </summary>
        public void AddTransition(INPCState from, INPCState to, params INPCTransition[] transitions)
        {
            for (int i = 0; i < transitions.Length; i++)
            {
                if (transitions[i] is IDisposable disposable) 
                    _toCleanup.Add(disposable);   
            }

            AddTransition(from, to, IsAllTrue(transitions));

            Func<bool> IsAllTrue(INPCTransition[] npcTransitions)
            {
                return () =>
                {
                    foreach (var npcTransition in npcTransitions)
                    {
                        if (!npcTransition.CanTransit())
                        {
                            return false;
                        }
                    }

                    return true;
                };
            }
        }

        public void AddAnyTransition(INPCState state, Func<bool> predicate)
        {
            _anyTransitions.Add(new Transition(state, predicate));
        }

        private class Transition
        {
            public Func<bool> Condition { get; }
            public INPCState To { get; }

            public Transition(INPCState to, Func<bool> condition)
            {
                To = to;
                Condition = condition;
            }
        }

        private Transition GetTransition()
        {
            foreach (var transition in _anyTransitions)
                if (transition.Condition())
                    return transition;

            foreach (var transition in _currentTransitions)
                if (transition.Condition())
                    return transition;

            return null;
        }
    }
}