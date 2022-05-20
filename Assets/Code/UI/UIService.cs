using RoM.Code.Core.Infrastructure.Services;
using UnityEngine;

namespace RoM.Code.UI
{
    public class UIService
    {
        private readonly IAssetProvider _assetProvider;

        public UIService(IAssetProvider assetProvider, Canvas uiRoot)
        {
            _assetProvider = assetProvider;
        }

        public void Open(string ui)
        {
            
        }

        public void Close(string ui)
        {
            
        }
    }
}