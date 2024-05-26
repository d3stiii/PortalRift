using TowerDefence.Common.UI;
using TowerDefence.Services.UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace TowerDefence.Meta.UI
{
  public class HomeScreen : BaseWindow
  {
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _settingsButton;
    private UIService _uiService;

    [Inject]
    public void Construct(UIService uiService) => 
      _uiService = uiService;

    protected override void Initialize()
    {
      _playButton.onClick.AddListener(OpenLevelSelectionScreen);
      _settingsButton.onClick.AddListener(OpenSettingsScreen);
      _quitButton.onClick.AddListener(Quit);
    }

    protected override void Cleanup()
    {
      _playButton.onClick.RemoveListener(OpenLevelSelectionScreen);
      _settingsButton.onClick.RemoveListener(OpenSettingsScreen);
      _quitButton.onClick.RemoveListener(Quit);
    }

    private void OpenLevelSelectionScreen() => 
      _uiService.Open<LevelSelectionScreen>();

    private void OpenSettingsScreen() => 
      _uiService.Open<SettingsScreen>();

    private void Quit() => 
      Application.Quit();
  }
}