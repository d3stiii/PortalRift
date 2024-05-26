using PortalRift.Configs;
using PortalRift.Runtime.Core.Pause;
using UnityEngine;

namespace PortalRift.Runtime.Core.Projectiles
{
  [RequireComponent(typeof(Projectile))]
  public class ProjectileMovement : MonoBehaviour, IPauseHandler
  {
    private Projectile _projectile;
    private float _speed;
    private Transform _target;
    private float _tempSpeed;

    public void Construct(ProjectileConfig projectileConfig, Transform target)
    {
      _target = target;
      _speed = projectileConfig.Speed;
    }

    private void Awake() => 
      _projectile = GetComponent<Projectile>();

    private void Update()
    {
      if (_target == null || !_target.gameObject.activeSelf)
      {
        ReturnToPool();
        return;
      }

      var target = _target.gameObject.transform.position;
      var position = transform.position;
      Vector2 direction = new(target.x - position.x, target.y - position.y);
      var rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
      transform.eulerAngles = new Vector3(0, 0, rotation);

      position = Vector3.MoveTowards(position,
        _target.transform.position,
        _speed * Time.deltaTime);

      transform.position = position;
    }

    public void SetPause(bool isPaused)
    {
      if (isPaused)
        _tempSpeed = _speed;

      _speed = isPaused ? 0 : _tempSpeed;
    }

    private void ReturnToPool()
    {
      _target = null;
      _projectile.ReturnToPool();
    }
  }
}