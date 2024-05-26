using System.Globalization;
using TMPro;
using TowerDefence.Core.Logic;
using UnityEngine;

namespace TowerDefence.Core.Hud
{
  public class HealthCounter : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _counter;
    private IHealth _health;

    public void Construct(IHealth health)
    {
      _health = health;
      UpdateCounter();
      health.HealthChanged += UpdateCounter;
    }

    private void UpdateCounter() => 
      _counter.text = ((int)_health.CurrentHp).ToString(CultureInfo.InvariantCulture);

    private void OnDestroy() => 
      _health.HealthChanged -= UpdateCounter;
  }
}