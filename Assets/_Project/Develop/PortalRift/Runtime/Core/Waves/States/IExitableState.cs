namespace PortalRift.Runtime.Core.Waves.States
{
  public interface IExitableState
  {
    void Tick();
    void Exit();
  }
}