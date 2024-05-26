using System;

namespace PortalRift.Runtime.Core.Logic
{
  public interface IHealth
  {
    event Action HealthChanged;
    float MaxHp { get; }
    float CurrentHp { get; }
    void TakeDamage(float damage);
  }
}