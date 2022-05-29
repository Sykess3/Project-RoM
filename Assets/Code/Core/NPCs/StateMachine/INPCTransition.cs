namespace RoM.Code.Core.NPCs.StateMachine
{
    public interface INPCTransition
    {   
        /// <summary>
        /// Invokes in FixedUpdate 
        /// </summary>
        /// <returns></returns>
        bool CanTransit();
    }
}