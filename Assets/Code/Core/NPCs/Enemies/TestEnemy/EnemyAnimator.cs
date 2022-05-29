using System;
using UnityEngine;

namespace RoM.Code.Core.NPCs.Enemies.TestEnemy
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public Action OnAttackPerformCallback;
        public Action OnAttackEndedCallback;
        
        private static readonly int DoJumpWhileRunning = Animator.StringToHash("DoJumpWhileRunning");
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int DoLeftPunch = Animator.StringToHash("DoLeftPunch");

        public void StartRunning()
        {
            _animator.SetBool(IsRunning,true);
        }

        public void StopRunning()
        {
            _animator.SetBool(IsRunning, false);
        }

        public void JumpWhileRunning()
        {
            _animator.SetTrigger(DoJumpWhileRunning);
        }

        public void LeftPunch()
        {
            _animator.SetTrigger(DoLeftPunch);
        }

        private void OnAttackPerform()
        {
            OnAttackPerformCallback?.Invoke();
        }
        private void OnAttackEnded()
        {
            OnAttackEndedCallback?.Invoke();
        }
    }
}