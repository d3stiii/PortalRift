using PortalRift.Runtime.Core.Waves;
using PortalRift.Runtime.Core.Waves.States;
using UnityEngine;
using VContainer;

namespace PortalRift.Runtime.Core.Levels
{
  public class Level : MonoBehaviour
  {
    private WaveStateMachine _waveStateMachine;

    [Inject]
    public void Construct(WaveStateMachine waveStateMachine) => 
      _waveStateMachine = waveStateMachine;

    private void Awake() => 
      _waveStateMachine.EnterState<WaitForWaveState>();

    private void Update() => 
      _waveStateMachine.Tick();
  }
}