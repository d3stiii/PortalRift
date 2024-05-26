using System;
using TowerDefence.Configs;
using TowerDefence.Core.Logic;
using TowerDefence.Data.Session;
using TowerDefence.Services.Audio;
using TowerDefence.Services.Data;
using UnityEngine;
using VContainer;

namespace TowerDefence.Core.Enemies
{
  public class EnemyHealth : MonoBehaviour, IHealth
  {
    public event Action HealthChanged;

    private AudioService _audioService;
    private int _baseHealth;
    private float _currentHp;
    private AudioClip _hitSound;
    private int _maxHp;
    private int _maxUpgradableHealth;
    private SessionData _sessionData;
    
    public float MaxHp => _maxHp;

    public float CurrentHp
    {
      get => _currentHp;
      private set
      {
        if (value == _currentHp) return;
        _currentHp = value;
        HealthChanged?.Invoke();
      }
    }

    [Inject]
    public void Construct(SessionDataProvider sessionDataProvider, AudioService audioService)
    {
      _sessionData = sessionDataProvider.SessionData;
      _audioService = audioService;
    }

    public void Configure(EnemyConfig config)
    {
      _maxUpgradableHealth = config.MaxUpgradableHealth;
      _baseHealth = config.Health;
      _hitSound = config.HitSound;

      ResetHealth();
    }

    private void OnEnable()
    {
      ResetHealth();
    }

    public void TakeDamage(float damage)
    {
      if (_currentHp <= 0)
        return;
      _audioService.PlaySfx(_hitSound);

      CurrentHp -= damage;
    }

    private void ResetHealth()
    {
      CurrentHp = _maxHp =
        Mathf.RoundToInt(_baseHealth + (_maxUpgradableHealth - _baseHealth) *
          Mathf.Clamp01(
            (float)(_sessionData.WavesData.CurrentWave - 1) / _sessionData.WavesData.MaxWave));
    }
  }
}