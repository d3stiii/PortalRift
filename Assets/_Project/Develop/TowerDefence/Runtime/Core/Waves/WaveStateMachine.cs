using System;
using System.Collections.Generic;
using TowerDefence.Core.Pause;
using TowerDefence.Core.Waves.States;

namespace TowerDefence.Core.Waves
{
  public class WaveStateMachine
  {
    private readonly Dictionary<Type, IExitableState> _cachedStates = new();
    private readonly PauseService _pauseService;
    private readonly StateFactory _stateFactory;
    private IExitableState _currentState;

    public WaveStateMachine(StateFactory stateFactory, PauseService pauseService)
    {
      _stateFactory = stateFactory;
      _pauseService = pauseService;
    }

    public void EnterState<TState>() where TState : class, IState
    {
      IState newState = ChangeState<TState>();
      newState.Enter();
    }

    public void EnterState<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      IPayloadedState<TPayload> newState = ChangeState<TState>();
      newState.Enter(payload);
    }

    public void Tick()
    {
      if (_pauseService.IsPaused)
        return;

      _currentState.Tick();
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _currentState?.Exit();

      var state = GetState<TState>();
      _currentState = state;

      return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState
    {
      if (_cachedStates.TryGetValue(typeof(TState), out var state))
        return state as TState;

      state = _stateFactory.CreateState<TState>();
      _cachedStates.Add(typeof(TState), state);
      return state as TState;
    }
  }
}