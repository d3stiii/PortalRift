using System;
using TowerDefence.Common.Pooling;
using TowerDefence.Configs;
using UnityEngine;

namespace TowerDefence.Core.Enemies
{
  [RequireComponent(typeof(EnemyMovement))]
  public class Enemy : MonoBehaviour
  {
    public event Action<Enemy> OnRelease;
    
    [SerializeField] private EnemyType _type;
    private EnemyMovement _movement;
    private ObjectPool<Enemy> _pool;
    public EnemyType Type => _type;

    public void Construct(ObjectPool<Enemy> pool) => 
      _pool = pool;

    private void Awake() => 
      _movement = GetComponent<EnemyMovement>();

    private void OnEnable() => 
      _movement.enabled = true;

    public void ReturnToPool()
    {
      _pool.Release(this);
      OnRelease?.Invoke(this);
    }
  }
}