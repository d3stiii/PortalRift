using System;
using PortalRift.Configs;
using PortalRift.Runtime.Core.Logic;
using PortalRift.Runtime.Services.Audio;
using UnityEngine;
using VContainer;

namespace PortalRift.Runtime.Core.Base
{
  public class BaseHealth : MonoBehaviour, IHealth
  {
    public event Action HealthChanged;
    
    private AudioService _audioService;
    private float _currentHealth;
    private AudioClip _damageSound;
    private int _maxHp;

    public float MaxHp => _maxHp;

    public float CurrentHp
    {
      get => _currentHealth;
      set
      {
        if (value == _currentHealth) return;
        _currentHealth = value;
        HealthChanged?.Invoke();
      }
    }

    [Inject]
    public void Configure(AudioService audioService) => 
      _audioService = audioService;

    public void Construct(BaseConfig config)
    {
      CurrentHp = _maxHp = config.Health;
      _damageSound = config.DamageSound;
    }

    public void TakeDamage(float damage)
    {
      if (CurrentHp <= 0)
        return;

      CurrentHp -= damage;
      _audioService.PlaySfx(_damageSound);
    }
  }
}