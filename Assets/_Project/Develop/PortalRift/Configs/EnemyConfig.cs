using PortalRift.Runtime.Core.Enemies;
using UnityEngine;

namespace PortalRift.Configs
{
  [CreateAssetMenu(menuName = "Configs/Enemies", fileName = "EnemyConfig")]
  public class EnemyConfig : ScriptableObject
  {
    [SerializeField] private EnemyType _type;
    [SerializeField] private Enemy _prefab;
    [SerializeField] private int _moneyForKill;
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private int _health;
    [SerializeField] private int _maxUpgradableHealth;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioClip _hitSound;

    public EnemyType Type => _type;
    public Enemy Prefab => _prefab;
    public int MoneyForKill => _moneyForKill;
    public int Damage => _damage;
    public int Health => _health;
    public int MaxUpgradableHealth => _maxUpgradableHealth;
    public float Speed => _speed;
    public AudioClip DeathSound => _deathSound;
    public AudioClip HitSound => _hitSound;
  }
}