using PortalRift.Configs;
using PortalRift.Runtime.Core.Base;
using UnityEngine;

namespace PortalRift.Runtime.Core.Enemies
{
  [RequireComponent(typeof(Enemy))]
  public class EnemyAttack : MonoBehaviour
  {
    private int _damage;
    private Enemy _enemy;

    public void Construct(EnemyConfig config) => 
      _damage = config.Damage;

    private void Awake() => 
      _enemy = GetComponent<Enemy>();

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.TryGetComponent<BaseHealth>(out var @base))
      {
        @base.TakeDamage(_damage);
        _enemy.ReturnToPool();
      }
    }
  }
}