using System.Collections.Generic;

namespace TowerDefence.Core.Pause
{
  public class PauseService
  {
    private readonly List<IPauseHandler> _pauseHandlers = new();

    public bool IsPaused { get; private set; }

    public void SetPaused(bool isPaused)
    {
      IsPaused = isPaused;

      foreach (var handler in _pauseHandlers)
        handler.SetPause(isPaused);
    }

    public void RegisterHandler(IPauseHandler pauseHandler)
    {
      if (_pauseHandlers.Contains(pauseHandler))
        return;

      _pauseHandlers.Add(pauseHandler);
    }

    public void UnregisterHandler(IPauseHandler pauseHandler) => 
      _pauseHandlers.Remove(pauseHandler);
  }
}