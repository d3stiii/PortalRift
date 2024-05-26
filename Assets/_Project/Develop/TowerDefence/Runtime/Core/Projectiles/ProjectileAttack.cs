using TowerDefence.Configs;
using TowerDefence.Core.Pause;
using UnityEngine;

namespace TowerDefence.Core.Projectiles
{
  [RequireComponent(typeof(Projectile))]
  public abstract class ProjectileAttack : MonoBehaviour, IPauseHandler
  {
    [SerializeField] protected Transform DamagePoint;
    [SerializeField] protected LayerMask EnemyMask;
    [SerializeField] private float _minDistance = 0.01f;
    private bool _isPaused;
    private Projectile _projectile;
    private Transform _target;
    protected ProjectileConfig Config;
    protected int Damage;

    public void Construct(ProjectileConfig config, int damage, Transform target)
    {
      _target = target;
      Config = config;
      Damage = damage;
    }

    private void Awake() => 
      _projectile = GetComponent<Projectile>();

    private void FixedUpdate()
    {
      if (_isPaused || Vector2.Distance(_target.position, transform.position) > _minDistance)
        return;

      Attack();
    }

    public void SetPause(bool isPaused) => 
      _isPaused = isPaused;

    protected void ReturnToPool() => 
      _projectile.ReturnToPool();

    protected abstract void Attack();
  }
}