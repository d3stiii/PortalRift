using PortalRift.Runtime.Common.UI;
using PortalRift.Runtime.Services.LevelManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;

namespace PortalRift.Runtime.Core.Results.UI
{
  public class WinWindow : BaseWindow
  {
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _nextLevelButton;
    private LevelSelector _levelSelector;

    [Inject]
    public void Construct(LevelSelector levelSelector) => 
      _levelSelector = levelSelector;

    protected override void Initialize()
    {
      _nextLevelButton.interactable = _levelSelector.NextLevelAvailable;

      _restartButton.onClick.AddListener(Restart);
      _homeButton.onClick.AddListener(ReturnHome);
      _nextLevelButton.onClick.AddListener(LoadNextLevel);
    }

    protected override void Cleanup()
    {
      _homeButton.onClick.RemoveListener(ReturnHome);
      _restartButton.onClick.RemoveListener(Restart);
      _nextLevelButton.onClick.RemoveListener(LoadNextLevel);
    }

    private void LoadNextLevel()
    {
      _levelSelector.SelectLevel(_levelSelector.CurrentLevelId + 1);
      Restart();
    }

    private void ReturnHome() => 
      SceneManager.LoadScene("Meta");

    private void Restart() => 
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}