using System.Globalization;
using PortalRift.Runtime.Core.Logic;
using TMPro;
using UnityEngine;

namespace PortalRift.Runtime.Core.Hud
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