﻿using TowerDefence.Common.Pooling;
using UnityEngine;

namespace TowerDefence.Core.Projectiles
{
  public class Projectile : MonoBehaviour
  {
    [SerializeField] private ProjectileType _type;
    private ObjectPool<Projectile> _objectPool;

    public ProjectileType Type => _type;

    public void Construct(ObjectPool<Projectile> objectPool) => 
      _objectPool = objectPool;

    public void ReturnToPool() => 
      _objectPool.Release(this);
  }
}