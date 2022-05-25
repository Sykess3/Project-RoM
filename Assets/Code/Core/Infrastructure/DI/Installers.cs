using MessagePipe;
using RoM.Code.Core.Infrastructure.MessagePipe.EventKeys;
using RoM.Code.Core.Infrastructure.Mirror;
using RoM.Code.Core.Infrastructure.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace RoM.Code.Core.Infrastructure
{
    public class MessagePipeInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            var options = builder.RegisterMessagePipe();

            // Setup GlobalMessagePipe to enable diagnostics window and global function
            builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));

            builder.RegisterMessageBroker<PlayerCharacterSpawned>(options);
        }
    }
    
    [System.Serializable]
    public class RootLifetimeMonoBehaviours : IInstaller
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private RoMNetworkManager _networkManager;
        
        public void Install(IContainerBuilder builder)
        {
            builder.UseComponents(components =>
            {
                components.AddInstance(_camera);
                components.AddInstance(_networkManager);
            });
        }
    }

    public class AssetsManagement: IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<IAssetProvider, BuiltInPrefabCloner>(Lifetime.Singleton);
        }
    }
}