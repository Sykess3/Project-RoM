using MessagePipe;
using RoM.Code.Core.Infrastructure.MessagePipe.EventKeys;
using RoM.Code.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace RoM.Code.Core.Infrastructure
{
    public class RootLifetime : LifetimeScope
    {
        [SerializeField] private RootLifetimeMonoBehaviours _rootLifetimeMonoBehaviours;
        protected override void Configure(IContainerBuilder builder)
        {
            var installers = new ExtraInstaller()
            {
                _rootLifetimeMonoBehaviours,
                new MessagePipeInstaller(),
                new AssetsManagement(),
            };
            
            builder.RegisterBuildCallback(resolver =>
            {
                var publisher = resolver.Resolve<ISubscriber<PlayerCharacterSpawned>>();
                publisher.Subscribe(x => Debug.Log(nameof(PlayerCharacterSpawned)));
            });
            
            installers.Install(builder);
        }
    }
    
    
}
