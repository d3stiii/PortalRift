using PortalRift.Runtime.Services.LevelManagement;
using UnityEngine;

namespace PortalRift.Runtime.Core.Waves.States
{
  public class WaitForWaveState : IState
  {
    private readonly LevelSelector _levelSelector;
    private readonly WaveStateMachine _stateMachine;
    private float _currentTime;

    public WaitForWaveState(WaveStateMachine stateMachine, LevelSelector levelSelector)
    {
      _stateMachine = stateMachine;
      _levelSelector = levelSelector;
    }

    public void Enter()
    {
    }

    public void Exit()
    {
    }

    public void Tick()
    {
      _currentTime += Time.deltaTime;

      if (_currentTime >= _levelSelector.CurrentLevelConfig.TimeBetweenWaves)
      {
        _stateMachine.EnterState<AttackState>();
        _currentTime = 0;
      }
    }
  }
}