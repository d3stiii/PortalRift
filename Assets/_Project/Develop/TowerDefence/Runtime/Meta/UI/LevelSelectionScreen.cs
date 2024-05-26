using TowerDefence.Common.Extensions;
using TowerDefence.Common.UI;
using TowerDefence.Services.Configs;
using TowerDefence.Services.Data;
using TowerDefence.Services.LevelManagement;
using TowerDefence.Services.UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace TowerDefence.Meta.UI
{
  public class LevelSelectionScreen : BaseWindow
  {
    [SerializeField] private Button _closeButton;
    [SerializeField] private RectTransform _levelButtonsContainer;
    [SerializeField] private LevelSelectionButton _levelSelectionButton;
    private ConfigProvider _configProvider;
    private LevelSelector _levelSelector;
    private PersistentDataProvider _persistentDataProvider;
    private UIService _uiService;

    [Inject]
    public void Construct(
      UIService uiService,
      ConfigProvider configProvider,
      LevelSelector levelSelector,
      PersistentDataProvider persistentDataProvider)
    {
      _persistentDataProvider = persistentDataProvider;
      _levelSelector = levelSelector;
      _configProvider = configProvider;
      _uiService = uiService;
    }

    protected override void Initialize()
    {
      _closeButton.onClick.AddListener(ReturnHome);
      GenerateLevelButtons();
    }

    private void GenerateLevelButtons()
    {
      var maxAvailableLevelIndex = _persistentDataProvider.PersistentData.CompletedLevelData.LastCompletedLevel + 1;

      for (var i = 0; i < _configProvider.Levels.Length; i++)
      {
        var levelId = i;
        var button = Instantiate(_levelSelectionButton, _levelButtonsContainer)
          .With(x => x.Construct(_levelSelector, levelId));

        button.GetComponent<Button>().interactable = i <= maxAvailableLevelIndex;
      }
    }

    protected override void Cleanup() => 
      _closeButton.onClick.RemoveListener(ReturnHome);

    private void ReturnHome() => 
      _uiService.Close<LevelSelectionScreen>();
  }
}