using UnityEditor.Animations;

namespace RoM.Code.Core.Animation
{
    public interface IAnimatorStateReader
    {
        void EnteredState(int hash);
        void ExitedState(int hash);
    
        AnimatorState State { get; }
    }
}