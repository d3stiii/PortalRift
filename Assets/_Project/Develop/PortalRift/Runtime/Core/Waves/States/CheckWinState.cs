using PortalRift.Runtime.Core.Results;
using PortalRift.Runtime.Services.Data;

namespace PortalRift.Runtime.Core.Waves.States
{
  public class CheckWinState : IState
  {
    private readonly GameResult _gameResult;
    private readonly SessionDataProvider _sessionDataProvider;
    private readonly WaveStateMachine _stateMachine;

    public CheckWinState(WaveStateMachine stateMachine, SessionDataProvider sessionDataProvider, GameResult gameResult)
    {
      _stateMachine = stateMachine;
      _sessionDataProvider = sessionDataProvider;
      _gameResult = gameResult;
    }

    public void Enter()
    {
    }

    public void Tick()
    {
      var wavesData = _sessionDataProvider.SessionData.WavesData;

      if (wavesData.CurrentWave == wavesData.MaxWave)
        _gameResult.Win();
      else
        _stateMachine.EnterState<WaitForWaveState>();
    }

    public void Exit()
    {
    }
  }
}