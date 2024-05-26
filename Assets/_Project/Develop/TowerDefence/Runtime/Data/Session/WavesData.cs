using System;

namespace TowerDefence.Data.Session
{
  public class WavesData
  {
    public event Action Changed;
    
    private int _currentWave;
    private int _maxWave;

    public int CurrentWave
    {
      get => _currentWave;
      set
      {
        _currentWave = value;
        Changed?.Invoke();
      }
    }

    public int MaxWave
    {
      get => _maxWave;
      set
      {
        _maxWave = value;
        Changed?.Invoke();
      }
    }
  }
}