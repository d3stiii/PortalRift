using TowerDefence.Core.Logic;
using UnityEngine;

namespace TowerDefence.Core.Projectiles
{
  [RequireComponent(typeof(Projectile))]
  public class SplashAttack : ProjectileAttack
  {
    private void OnDrawGizmos()
    {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, Config.DamageRadius);
    }

    protected override void Attack()
    {
      var enemies = Physics2D.OverlapCircleAll(DamagePoint.position, Config.DamageRadius, EnemyMask);
      if (enemies.Length <= 0)
        return;

      foreach (var enemy in enemies)
      {
        if (enemy == null || !enemy.gameObject.activeSelf)
          continue;

        var distance = Vector2.Distance(enemy.transform.position,
          transform.position);

        var calculatedDamage = Damage - Damage *
          Mathf.Clamp01(Mathf.Sqrt(distance / Config.DamageRadius));

        enemy.GetComponent<IHealth>().TakeDamage(calculatedDamage);
      }

      ReturnToPool();
    }
  }
}