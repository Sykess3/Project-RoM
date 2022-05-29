using RoM.Code.Core.NPCs.Enemies.StateMachineAbstractions;
using UnityEngine;

namespace RoM.Code.Core.NPCs.Enemies
{
    public class Enemy : MonoBehaviour, IFollower, IAttackable
    {
        public Transform Target { get; set; }
        
        public void Attack(IDamageable target)
        {
            target.TakeDamage(10);
        }
    }
}