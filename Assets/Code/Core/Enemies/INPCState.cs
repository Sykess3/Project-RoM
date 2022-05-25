namespace RoM.Code.Core.Enemy
{
    public interface INPCState
    {
        void Tick();
        void OnEnter();
        void OnExit();
    }
}