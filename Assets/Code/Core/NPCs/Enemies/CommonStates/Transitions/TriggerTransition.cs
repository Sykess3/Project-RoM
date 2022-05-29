using System;
using Code.Utils;
using RoM.Code.Core.NPCs.StateMachine;
using RoM.Code.Utils;
using UnityEngine;

namespace RoM.Code.Core.NPCs.Enemies.CommonStates.Transitions
{
    public static class TriggerTransition
    {
        public class OnEnter : INPCTransition, IDisposable
        {
            private readonly Func<Collider, bool> _extraCheck;
            private readonly TriggerObservable _trigger;
            private readonly float _delayTime;
            private readonly Timer _timer;

            public OnEnter(TriggerObservable trigger, float delayTime = 0)
            {
                _trigger = trigger;
                _delayTime = delayTime;
                _timer = new Timer();
                _trigger.Entered += OnEntered;
                _trigger.Exited += OnExited;
            }

            public OnEnter(TriggerObservable trigger, Func<Collider, bool> extraCheck, float delayTime = 0)
                : this(trigger, delayTime)
            {
                _extraCheck = extraCheck;
            }

            public bool CanTransit() => _timer.IsReached;

            private void OnExited(Collider obj)
            {

                if (IsExtraCheckCorrect(obj))
                {
                    _timer.Break();
                }
                else
                {
                    _timer.Start(_delayTime);
                }
            }

            private bool IsExtraCheckCorrect(Collider collider)
            {
                bool? predicate = _extraCheck?.Invoke(collider);
                return predicate != null && predicate.Value;
            }

            private void OnEntered(Collider obj)
            {
                if (IsExtraCheckCorrect(obj))
                {
                    _timer.Start(_delayTime);
                }
            }

            public void Dispose()
            {
                _trigger.Entered -= OnEntered;
                _trigger.Exited -= OnExited;
            }
        }

        public class OnExit : INPCTransition, IDisposable
        {
            private readonly Func<Collider, bool> _extraCheck;
            private readonly TriggerObservable _trigger;
            private readonly float _delayTime;
            private readonly Timer _timer;

            public bool CanTransit() => _timer.IsReached;

            public OnExit(TriggerObservable trigger, float delayTime = 0)
            {
                _trigger = trigger;
                _delayTime = delayTime;
                _timer = new Timer();
                _trigger.Entered += OnEntered;
                _trigger.Exited += OnExited;
            }

            public OnExit(TriggerObservable trigger, Func<Collider, bool> extraCheck, float delayTime = 0)
                : this(trigger, delayTime)
            {
                _extraCheck = extraCheck;
            }

            private void OnExited(Collider obj)
            {
                if (IsExtraCheckCorrect(obj))
                {
                    _timer.Start(_delayTime);   
                }
                else
                {
                    _timer.Break();
                }
            }

            private bool IsExtraCheckCorrect(Collider collider)
            {
                bool? predicate = _extraCheck?.Invoke(collider);
                return predicate != null && predicate.Value;
            }

            private void OnEntered(Collider obj)
            {
                _timer.Break(); 
            }


            public void Dispose()
            {
                _trigger.Entered -= OnEntered;
                _trigger.Exited -= OnExited;
            }
        }
    }
}