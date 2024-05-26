using TowerDefence.Common.Extensions;
using TowerDefence.Common.Pooling;
using TowerDefence.Core.Pause;
using TowerDefence.Services.Configs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace TowerDefence.Core.Projectiles
{
  public class ProjectileFactory
  {
    private readonly ConfigProvider _configProvider;
    private readonly IObjectResolver _container;
    private readonly PauseService _pauseService;
    private readonly ObjectPool<Projectile> _projectilesPool;
    private ProjectileType _nextProjectileType;

    public ProjectileFactory(IObjectResolver container, PauseService pauseService, ConfigProvider configProvider)
    {
      _container = container;
      _pauseService = pauseService;
      _configProvider = configProvider;
      _projectilesPool = new ObjectPool<Projectile>(CreateProjectile,
        proj => proj.gameObject.SetActive(true),
        proj => proj.gameObject.SetActive(false));
    }

    public Projectile GetOrCreateProjectile(ProjectileType type, Vector3 at, int damage, Transform target)
    {
      _nextProjectileType = type;

      var config = _configProvider.GetProjectileConfig(type);

      var projectile = _projectilesPool.Get(x => type == x.Type)
        .With(x => x.transform.position = at)
        .With(x => x.GetComponent<ProjectileAttack>().Construct(config, damage, target))
        .With(x => x.GetComponent<ProjectileMovement>().Construct(config, target));

      return projectile;
    }

    private Projectile CreateProjectile()
    {
      var prefab = _configProvider.GetProjectileConfig(_nextProjectileType).Prefab;

      var projectile = _container
        .Instantiate(prefab)
        .With(x => x.Construct(_projectilesPool));

      foreach (var handler in projectile.GetComponents<IPauseHandler>())
        _pauseService.RegisterHandler(handler);

      return projectile;
    }

    public void Cleanup() => 
      _projectilesPool.Clear();
  }
}