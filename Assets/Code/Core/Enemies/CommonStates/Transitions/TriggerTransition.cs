using System;
using Code.Utils;
using RoM.Code.Utils;
using UnityEngine;

namespace RoM.Code.Core.Enemies.CommonStates.Transitions
{
    public static class TriggerTransition
    {
        public class OnEnter : INPCTransition, IDisposable
        {
            private readonly Func<Collider, bool> _extraCheck;
            private readonly TriggerObservable _trigger;
            private readonly float _additionTime;
            private readonly Timer _timer;

            public OnEnter(TriggerObservable trigger, float additionTime = 0)
            {
                _trigger = trigger;
                _additionTime = additionTime;
                _timer = new Timer();
                _trigger.Entered += OnEntered;
                _trigger.Exited += OnExited;
            }

            public OnEnter(TriggerObservable trigger, Func<Collider, bool> extraCheck, float additionTime = 0)
                : this(trigger, additionTime)
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
                    _timer.Start(_additionTime);
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
                    _timer.Start(_additionTime);
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
            private readonly float _additionTime;
            private readonly Timer _timer;

            public bool CanTransit() => _timer.IsReached;

            public OnExit(TriggerObservable trigger, float additionTime = 0)
            {
                _trigger = trigger;
                _additionTime = additionTime;
                _timer = new Timer();
                _trigger.Entered += OnEntered;
                _trigger.Exited += OnExited;
            }

            public OnExit(TriggerObservable trigger, Func<Collider, bool> extraCheck, float additionTime = 0)
                : this(trigger, additionTime)
            {
                _extraCheck = extraCheck;
            }

            private void OnExited(Collider obj)
            {

                if (IsExtraCheckCorrect(obj))
                {
                    _timer.Start(_additionTime);   
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
                if (IsExtraCheckCorrect(obj))
                {
                    _timer.Break();   
                }
            }


            public void Dispose()
            {
                _trigger.Entered -= OnEntered;
                _trigger.Exited -= OnExited;
            }
        }
    }
}