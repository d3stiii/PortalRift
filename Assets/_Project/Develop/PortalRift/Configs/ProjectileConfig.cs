using PortalRift.Runtime.Core.Projectiles;
using UnityEngine;

namespace PortalRift.Configs
{
  [CreateAssetMenu(menuName = "Configs/Towers/Projectile", fileName = "ProjectileConfig")]
  public class ProjectileConfig : ScriptableObject
  {
    [SerializeField] private ProjectileType _type;
    [SerializeField] private float _speed;
    [SerializeField] private float _damageRadius;
    [SerializeField] private Projectile _prefab;

    public ProjectileType Type => _type;
    public float Speed => _speed;
    public float DamageRadius => _damageRadius;
    public Projectile Prefab => _prefab;
  }
}