using PortalRift.Runtime.Common.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PortalRift.Runtime.Core.Results.UI
{
  public class LoseWindow : BaseWindow
  {
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _returnHomeButton;

    protected override void Initialize()
    {
      _restartButton.onClick.AddListener(Restart);
      _returnHomeButton.onClick.AddListener(ReturnHome);
    }

    protected override void Cleanup()
    {
      _restartButton.onClick.RemoveListener(Restart);
      _returnHomeButton.onClick.RemoveListener(ReturnHome);
    }

    private void Restart() => 
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    private void ReturnHome() => 
      SceneManager.LoadScene("Meta");
  }
}