using PortalRift.Configs;
using PortalRift.Runtime.Core.Pause;
using UnityEngine;

namespace PortalRift.Runtime.Core.Enemies
{
  [RequireComponent(typeof(Enemy), typeof(EnemyAnimator))]
  public class EnemyMovement : MonoBehaviour, IPauseHandler
  {
    [SerializeField] private float _minDistanceToPoint = 0.01f;
    private EnemyAnimator _animator;
    private int _currentPoint;
    private Enemy _pooledEnemy;
    private float _speed;
    private float _tempSpeed;
    private Vector3[] _waypoints;

    public void Construct(Vector3[] waypoints, EnemyConfig enemyConfig)
    {
      _waypoints = waypoints;
      _speed = _tempSpeed = enemyConfig.Speed;
    }

    private void Awake()
    {
      _pooledEnemy = GetComponent<Enemy>();
      _animator = GetComponent<EnemyAnimator>();
    }

    private void Update()
    {
      var nextPoint = _waypoints[_currentPoint];

      if (Vector2.Distance(transform.position, nextPoint) <= _minDistanceToPoint)
      {
        ++_currentPoint;
        if (_currentPoint >= _waypoints.Length)
        {
          _pooledEnemy.ReturnToPool();
          return;
        }
      }

      transform.position = Vector3.MoveTowards(transform.position, nextPoint, _speed * Time.deltaTime);
      _animator.StartMoving((nextPoint - transform.position).normalized);
    }

    private void OnEnable() => 
      _currentPoint = 0;

    public void SetPause(bool isPaused)
    {
      if (isPaused)
        _tempSpeed = _speed;

      _speed = isPaused ? 0 : _tempSpeed;
    }
  }
}