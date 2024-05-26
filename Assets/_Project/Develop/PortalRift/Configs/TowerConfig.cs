using System.Collections.Generic;
using PortalRift.Runtime.Core.Projectiles;
using PortalRift.Runtime.Core.Towers;
using UnityEngine;

namespace PortalRift.Configs
{
  [CreateAssetMenu(menuName = "Configs/Towers/Create Tower Parameter")]
  public class TowerConfig : ScriptableObject
  {
    [SerializeField] private TowerType _type;
    [SerializeField] private ProjectileType _projectileType;
    [SerializeField] private Tower _prefab;
    [SerializeField] private int _cost;
    [SerializeField] private List<UpgradeLevel> _upgradeLevels;
    [SerializeField] private AudioClip _shotSound;

    public TowerType Type => _type;
    public Tower Prefab => _prefab;
    public ProjectileType ProjectileType => _projectileType;
    public int Cost => _cost;
    public List<UpgradeLevel> UpgradeLevels => _upgradeLevels;
    public AudioClip ShotSound => _shotSound;
  }
}