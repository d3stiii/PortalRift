using TMPro;
using TowerDefence.Services.LevelManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefence.Meta.UI
{
  [RequireComponent(typeof(Button))]
  public class LevelSelectionButton : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _levelIdText;
    private Button _button;
    private int _levelId;
    private LevelSelector _levelSelector;

    public void Construct(LevelSelector levelSelector, int levelId)
    {
      _levelSelector = levelSelector;
      _levelId = levelId;
      _levelIdText.text = (_levelId + 1).ToString();
    }

    private void Awake()
    {
      _button = GetComponent<Button>();
      _button.onClick.AddListener(LoadLevel);
    }

    private void OnDestroy() => 
      _button.onClick.RemoveListener(LoadLevel);

    private void LoadLevel()
    {
      _levelSelector.SelectLevel(_levelId);
      SceneManager.LoadScene("Core");
    }
  }
}