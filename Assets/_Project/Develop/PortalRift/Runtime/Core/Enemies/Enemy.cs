using System;
using PortalRift.Configs;
using PortalRift.Runtime.Common.Pooling;
using UnityEngine;

namespace PortalRift.Runtime.Core.Enemies
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