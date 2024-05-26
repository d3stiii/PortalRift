using VContainer;
using VContainer.Unity;

namespace TowerDefence.Bootstrap
{
  public class BootstrapScope : LifetimeScope
  {
    protected override void Configure(IContainerBuilder builder) => 
      builder.RegisterEntryPoint<BootstrapEntryPoint>();
  }
}