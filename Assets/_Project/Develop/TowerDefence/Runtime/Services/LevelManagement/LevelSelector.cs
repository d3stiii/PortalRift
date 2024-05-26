using TowerDefence.Configs;
using TowerDefence.Services.Configs;
using UnityEngine;

namespace TowerDefence.Services.LevelManagement
{
  public class LevelSelector
  {
    private readonly ConfigProvider _configProvider;

    public LevelSelector(ConfigProvider configProvider) => 
      _configProvider = configProvider;

    public LevelConfig CurrentLevelConfig => _configProvider.Levels[CurrentLevelId];
    public int CurrentLevelId { get; private set; }

    public bool NextLevelAvailable => _configProvider.Levels.Length > CurrentLevelId + 1;

    public void SelectLevel(int id)
    {
      if (id >= _configProvider.Levels.Length || id < 0)
      {
        Debug.LogError($"Level Id: {id} is incorrect");
        return;
      }

      CurrentLevelId = id;
    }
  }
}