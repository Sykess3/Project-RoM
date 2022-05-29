namespace RoM.Code.Core.NPCs.Enemies.StateMachineAbstractions
{
    public interface IAttackable
    {
        void Attack(IDamageable target);
    }
}