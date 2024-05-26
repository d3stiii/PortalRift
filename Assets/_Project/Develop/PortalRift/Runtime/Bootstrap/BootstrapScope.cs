using VContainer;
using VContainer.Unity;

namespace PortalRift.Runtime.Bootstrap
{
  public class BootstrapScope : LifetimeScope
  {
    protected override void Configure(IContainerBuilder builder) => 
      builder.RegisterEntryPoint<BootstrapEntryPoint>();
  }
}