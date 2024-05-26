using TMPro;
using TowerDefence.Services.Data;
using UnityEngine;
using VContainer;

namespace TowerDefence.Core.Hud
{
  public class WaveCounter : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _counter;
    private SessionDataProvider _sessionDataProvider;

    [Inject]
    public void Construct(SessionDataProvider sessionDataProvider)
    {
      _sessionDataProvider = sessionDataProvider;
      _sessionDataProvider.SessionData.WavesData.Changed += UpdateCounter;
    }

    private void Awake() => 
      UpdateCounter();

    private void UpdateCounter() => 
      _counter.text = _sessionDataProvider.SessionData.WavesData.CurrentWave.ToString();

    private void OnDestroy() => 
      _sessionDataProvider.SessionData.WavesData.Changed -= UpdateCounter;
  }
}