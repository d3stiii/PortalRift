namespace PortalRift.Runtime.Core.Waves.States
{
  public interface IPayloadedState<TPayload> : IExitableState
  {
    void Enter(TPayload payload);
  }
}