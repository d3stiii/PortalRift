using PortalRift.Runtime.Services.UI;
using VContainer;
using VContainer.Unity;

namespace PortalRift.Runtime.Meta
{
  public class MetaScope : LifetimeScope
  {
    protected override void Configure(IContainerBuilder builder)
    {
      builder.RegisterEntryPoint<MetaEntryPoint>();
      BindUI(builder);
    }

    private static void BindUI(IContainerBuilder builder)
    {
      builder.Register<UIFactory>(Lifetime.Singleton);
      builder.Register<UIService>(Lifetime.Singleton);
    }
  }
}