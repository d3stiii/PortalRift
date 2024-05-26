using PortalRift.Runtime.Core.Pause;
using PortalRift.Runtime.Core.Results.UI;
using PortalRift.Runtime.Services.Data;
using PortalRift.Runtime.Services.LevelManagement;
using PortalRift.Runtime.Services.SaveLoad;
using PortalRift.Runtime.Services.UI;

namespace PortalRift.Runtime.Core.Results
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

      _persistentDataProvider.PersistentData.PlayerProgress.CompleteLevel(_levelSelector.CurrentLevelId);
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