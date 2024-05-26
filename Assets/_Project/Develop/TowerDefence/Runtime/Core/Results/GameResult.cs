using TowerDefence.Core.Pause;
using TowerDefence.Core.Results.UI;
using TowerDefence.Services.Data;
using TowerDefence.Services.LevelManagement;
using TowerDefence.Services.SaveLoad;
using TowerDefence.Services.UI;

namespace TowerDefence.Core.Results
{
  public class GameResult
  {
    private readonly LevelSelector _levelSelector;
    private readonly PauseService _pauseService;
    private readonly PersistentDataProvider _persistentDataProvider;
    private readonly SaveLoadService _saveLoadService;
    private readonly UIService _uiService;

    public GameResult(
      UIService uiService,
      PauseService pauseService,
      PersistentDataProvider persistentDataProvider,
      LevelSelector levelSelector,
      SaveLoadService saveLoadService)
    {
      _pauseService = pauseService;
      _persistentDataProvider = persistentDataProvider;
      _levelSelector = levelSelector;
      _saveLoadService = saveLoadService;
      _uiService = uiService;
    }

    public void Win()
    {
      _pauseService.SetPaused(true);
      _uiService.CloseAll();
      _uiService.Open<WinWindow>();

      _persistentDataProvider.PersistentData.CompletedLevelData.CompleteLevel(_levelSelector.CurrentLevelId);
      _saveLoadService.Save();
    }

    public void Lose()
    {
      _pauseService.SetPaused(true);
      _uiService.CloseAll();
      _uiService.Open<LoseWindow>();
    }
  }
}