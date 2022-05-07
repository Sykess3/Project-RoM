using System;
using RoM.Code.Core.Infrastructure.AppInitialization.Steps;

namespace RoM.Code.Core.Infrastructure.AppInitialization
{
    public class AppInitialization
    {
        public void StartInit()
        {
            
        }


        private void Order(BaseInitializationStep[] steps)
        {
            for (int i = 0; i < steps.Length; i++)
            {
                if (i < steps.Length - 1)
                {
                    steps[i] = steps[i + 1];   
                }
            }
        }
    }
}