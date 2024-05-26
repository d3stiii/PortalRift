using PortalRift.Runtime.Core.Logic;
using UnityEngine;

namespace PortalRift.Runtime.Core.Projectiles
{
  [RequireComponent(typeof(Projectile))]
  public class SingleTargetAttack : ProjectileAttack
  {
    protected override void Attack()
    {
      var enemy = Physics2D.OverlapCircle(DamagePoint.position, Config.DamageRadius, EnemyMask);
      if (enemy == null || !enemy.gameObject.activeSelf)
        return;

      enemy.GetComponent<IHealth>().TakeDamage(Damage);
      ReturnToPool();
    }
  }
}