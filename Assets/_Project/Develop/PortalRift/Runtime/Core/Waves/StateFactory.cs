using PortalRift.Runtime.Core.Waves.States;
using VContainer;

namespace PortalRift.Runtime.Core.Waves
{
  public class StateFactory
  {
    private readonly IObjectResolver _container;

    public StateFactory(IObjectResolver container) => 
      _container = container;

    public IExitableState CreateState<TState>() where TState : IExitableState => 
      _container.Resolve<TState>();
  }
}