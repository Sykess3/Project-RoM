namespace RoM.Code.Core.Enemies
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