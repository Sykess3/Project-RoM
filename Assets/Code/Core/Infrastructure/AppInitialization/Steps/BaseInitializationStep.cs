namespace RoM.Code.Core.Infrastructure.AppInitialization.Steps
{
    public abstract class BaseInitializationStep
    {
        public BaseInitializationStep NextStep { get; }
        
        public abstract void RunStep();

        protected virtual void Dispose()
        {
        }

        protected void OnStepComplete()
        {
            Dispose();
            NextStep?.RunStep();
        }
    }
}