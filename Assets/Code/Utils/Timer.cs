using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Utils
{
    public class Timer
    {
        private float _goal;
        private bool _isBreak = true;
        public bool IsReached => _goal <= Time.time && !_isBreak;
        public void Start(float time)
        {
            _goal = Time.time + time;
            _isBreak = false;
        }

        public void Break()
        {
            _isBreak = true;
        }
    }
}