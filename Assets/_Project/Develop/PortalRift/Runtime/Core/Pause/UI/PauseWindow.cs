using PortalRift.Runtime.Common.UI;
using PortalRift.Runtime.Services.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;

namespace PortalRift.Runtime.Core.Pause.UI
{
  public class PauseWindow : BaseWindow
  {
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _restartButton;
    private PauseService _pauseService;
    private UIService _uiService;

    [Inject]
    public void Construct(PauseService pauseService, UIService uiService)
    {
      _pauseService = pauseService;
      _uiService = uiService;
    }

    protected override void Initialize()
    {
      _resumeButton.onClick.AddListener(Resume);
      _homeButton.onClick.AddListener(ReturnHome);
      _restartButton.onClick.AddListener(Restart);
    }

    protected override void Cleanup()
    {
      _resumeButton.onClick.RemoveListener(Resume);
      _homeButton.onClick.RemoveListener(ReturnHome);
    }

    private void Restart() => 
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    private void Resume()
    {
      _pauseService.SetPaused(false);
      _uiService.Close<PauseWindow>();
    }

    private void ReturnHome() => 
      SceneManager.LoadScene("Meta");
  }
}