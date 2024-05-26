using PortalRift.Runtime.Core.Logic;
using UnityEngine;
using UnityEngine.UI;

namespace PortalRift.Runtime.Core.Enemies.UI
{
  public class HealthBar : MonoBehaviour
  {
    [SerializeField] private Image _currentHealthImage;
    private IHealth _health;

    private void Construct(IHealth health)
    {
      _health = health;
      UpdateBar();
      health.HealthChanged += UpdateBar;
    }

    private void Awake() => 
      Construct(GetComponentInParent<IHealth>());

    private void UpdateBar() => 
      _currentHealthImage.fillAmount = _health.CurrentHp / _health.MaxHp;

    private void OnDestroy() => 
      _health.HealthChanged -= UpdateBar;
  }
}