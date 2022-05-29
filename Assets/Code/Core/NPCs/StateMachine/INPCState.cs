namespace RoM.Code.Core.NPCs.StateMachine
{
    public interface INPCState
    {
        void Tick();
        void OnEnter();
        void OnExit();
    }
}