using TowerDefence.Common.Extensions;
using TowerDefence.Common.Pooling;
using TowerDefence.Configs;
using TowerDefence.Core.Pause;
using TowerDefence.Services.Configs;
using TowerDefence.Services.LevelManagement;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace TowerDefence.Core.Enemies
{
  public class EnemyFactory
  {
    private readonly ConfigProvider _configProvider;
    private readonly IObjectResolver _container;
    private readonly ObjectPool<Enemy> _enemiesPool;
    private readonly LevelSelector _levelSelector;
    private readonly PauseService _pauseService;
    private EnemyType _nextEnemyType;

    public EnemyFactory(
      IObjectResolver container,
      ConfigProvider configProvider,
      LevelSelector levelSelector,
      PauseService pauseService)
    {
      _pauseService = pauseService;
      _container = container;
      _configProvider = configProvider;
      _levelSelector = levelSelector;
      _enemiesPool = new ObjectPool<Enemy>(CreateEnemy,
        null,
        enemy => enemy.gameObject.SetActive(false));
    }

    public Enemy GetOrCreateEnemy(EnemyType enemyType, Vector3 at)
    {
      _nextEnemyType = enemyType;
      var enemy = _enemiesPool.Get(x => x.Type == enemyType);
      enemy.transform.position = at;
      enemy.gameObject.SetActive(true);
      return enemy;
    }

    private Enemy CreateEnemy()
    {
      var enemyConfig = _configProvider.GetEnemyConfig(_nextEnemyType);
      var prefab = enemyConfig.Prefab;
      var enemy = _container.Instantiate(prefab)
        .With(x => x.GetComponent<Enemy>()
          .Construct(_enemiesPool))
        .With(x => x.GetComponent<EnemyMovement>()
          .Construct(_levelSelector.CurrentLevelConfig.EnemyWaypoints.ToArray(), enemyConfig))
        .With(x => x.GetComponent<EnemyHealth>()
          .Configure(enemyConfig))
        .With(x => x.GetComponent<EnemyAttack>()
          .Construct(enemyConfig))
        .With(x => x.GetComponent<EnemyDeath>()
          .Construct(enemyConfig))
        .With(x => x.GetComponent<EnemyDeathReward>()
          .Construct(enemyConfig));

      foreach (var handler in enemy.GetComponents<IPauseHandler>())
        _pauseService.RegisterHandler(handler);

      return enemy;
    }

    public void Cleanup() => 
      _enemiesPool.Clear();
  }
}