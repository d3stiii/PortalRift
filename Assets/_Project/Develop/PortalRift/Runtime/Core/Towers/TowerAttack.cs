using System.Linq;
using PortalRift.Configs;
using PortalRift.Runtime.Core.Enemies;
using PortalRift.Runtime.Core.Pause;
using PortalRift.Runtime.Core.Projectiles;
using PortalRift.Runtime.Services.Audio;
using UnityEngine;
using VContainer;

namespace PortalRift.Runtime.Core.Towers
{
  [RequireComponent(typeof(TowerUpgrading))]
  public class TowerAttack : MonoBehaviour
  {
    [SerializeField] private LayerMask _enemyLayer;
    private AudioService _audioService;
    private float _cooldown;
    private Collider2D[] _enemiesBuffer = new Collider2D[15];
    private Enemy _focusedEnemy;
    private PauseService _pauseService;
    private ProjectileFactory _projectileFactory;
    private ProjectileType _projectileType;
    private AudioClip _shotSound;
    private TowerUpgrading _towerUpgrading;

    private Stats Stats => _towerUpgrading.CurrentUpgradeData.Stats;

    [Inject]
    public void Construct(ProjectileFactory projectileFactory, PauseService pauseService, AudioService audioService)
    {
      _pauseService = pauseService;
      _projectileFactory = projectileFactory;
      _audioService = audioService;
    }

    public void Configure(TowerConfig towerConfig)
    {
      _projectileType = towerConfig.ProjectileType;
      _shotSound = towerConfig.ShotSound;
    }

    private void Awake() =>
      _towerUpgrading = GetComponent<TowerUpgrading>();

    private void Update()
    {
      if (_pauseService.IsPaused)
        return;

      if (_cooldown > 0)
        _cooldown -= Time.deltaTime;

      if (_focusedEnemy == null || _cooldown > 0)
        return;

      _cooldown = Stats.Cooldown;

      _projectileFactory
        .GetOrCreateProjectile(_projectileType, transform.position, Stats.Damage, _focusedEnemy.transform);
      _audioService.PlaySfx(_shotSound);
    }

    private void FixedUpdate()
    {
      if (_pauseService.IsPaused)
        return;

      _enemiesBuffer = Physics2D.OverlapCircleAll(transform.position, Stats.Radius, _enemyLayer);

      if (_focusedEnemy != null && FocusedEnemyInRange())
        return;

      _focusedEnemy = _enemiesBuffer
        .FirstOrDefault(x => x != null && x.gameObject.activeSelf)?
        .GetComponent<Enemy>();
    }

    private bool FocusedEnemyInRange()
    {
      return _enemiesBuffer.Any(x =>
      {
        if (x == null || !x.gameObject.activeSelf)
          return false;
        return x.GetComponent<Enemy>() == _focusedEnemy;
      });
    }

    private void OnDrawGizmos()
    {
      Gizmos.color = Color.green;
      Gizmos.DrawWireSphere(transform.position, Stats.Radius);
    }
  }
}