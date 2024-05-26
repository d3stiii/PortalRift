using System;
using System.Collections;
using TowerDefence.Configs;
using TowerDefence.Services.Audio;
using UnityEngine;
using VContainer;

namespace TowerDefence.Core.Enemies
{
  [RequireComponent(typeof(Enemy))]
  [RequireComponent(typeof(EnemyMovement))]
  [RequireComponent(typeof(EnemyHealth))]
  [RequireComponent(typeof(EnemyAnimator))]
  public class EnemyDeath : MonoBehaviour
  {
    public event Action Died;
    
    private EnemyAnimator _animator;
    private AudioService _audioService;
    private AudioClip _deathSound;
    private EnemyHealth _enemyHealth;
    private EnemyMovement _movement;
    private Enemy _pooledEnemy;

    [Inject]
    public void Configure(AudioService audioService) => 
      _audioService = audioService;

    public void Construct(EnemyConfig config) => 
      _deathSound = config.DeathSound;

    private void Awake()
    {
      _pooledEnemy = GetComponent<Enemy>();
      _enemyHealth = GetComponent<EnemyHealth>();
      _animator = GetComponent<EnemyAnimator>();
      _movement = GetComponent<EnemyMovement>();
      _enemyHealth.HealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged()
    {
      if (_enemyHealth.CurrentHp <= 0)
        Die();
    }

    private void Die()
    {
      _audioService.PlaySfx(_deathSound);
      _movement.enabled = false;
      _animator.StopMoving();
      _animator.PlayDeath();
      StartCoroutine(ReturnToPool());
      Died?.Invoke();
    }

    private IEnumerator ReturnToPool()
    {
      yield return new WaitForSeconds(0.5f);
      _pooledEnemy.ReturnToPool();
    }
  }
}