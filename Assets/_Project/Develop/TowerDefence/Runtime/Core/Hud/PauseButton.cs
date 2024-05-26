using TowerDefence.Core.Pause;
using TowerDefence.Core.Pause.UI;
using TowerDefence.Services.UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace TowerDefence.Core.Hud
{
  [RequireComponent(typeof(Button))]
  public class PauseButton : MonoBehaviour
  {
    private Button _pauseButton;
    private PauseService _pauseService;
    private UIService _uiService;

    [Inject]
    public void Construct(PauseService pauseService, UIService uiService)
    {
      _uiService = uiService;
      _pauseService = pauseService;
    }

    private void Awake()
    {
      _pauseButton = GetComponent<Button>();
      _pauseButton.onClick.AddListener(SetPause);
    }

    private void SetPause()
    {
      _uiService.CloseAll();
      _pauseService.SetPaused(true);
      _uiService.Open<PauseWindow>();
    }

    private void OnDestroy() => 
      _pauseButton.onClick.RemoveListener(SetPause);
  }
}