using TowerDefence.Configs;
using TowerDefence.Core.Enemies;
using TowerDefence.Services.Data;
using TowerDefence.Services.LevelManagement;
using UnityEngine;

namespace TowerDefence.Core.Waves.States
{
  public class AttackState : IState
  {
    private readonly EnemyFactory _enemyFactory;
    private readonly LevelSelector _levelSelector;
    private readonly SessionDataProvider _sessionDataProvider;
    private readonly WaveStateMachine _stateMachine;
    private WaveSettings _currentWave;
    private int _enemiesAlive;
    private int _enemiesLeftToSpawn;
    private float _spawnTimer;

    public AttackState(
      WaveStateMachine stateMachine,
      LevelSelector levelSelector,
      EnemyFactory enemyFactory,
      SessionDataProvider sessionDataProvider)
    {
      _stateMachine = stateMachine;
      _levelSelector = levelSelector;
      _enemyFactory = enemyFactory;
      _sessionDataProvider = sessionDataProvider;
    }

    public void Enter()
    {
      var currentWaveIndex = _sessionDataProvider.SessionData.WavesData.CurrentWave++;
      _currentWave = _levelSelector.CurrentLevelConfig.Waves[currentWaveIndex];
      _enemiesLeftToSpawn = _currentWave.EnemiesCount;
      _enemiesAlive = 0;
    }

    public void Tick()
    {
      _spawnTimer += Time.deltaTime;

      if (_enemiesLeftToSpawn > 0 && _spawnTimer >= 60f / _currentWave.EnemySpawnRate)
        SpawnEnemy();

      if (_enemiesLeftToSpawn == 0 && _enemiesAlive == 0)
        _stateMachine.EnterState<CheckWinState>();
    }

    public void Exit()
    {
    }

    private void DecreaseAliveEnemies(Enemy enemy)
    {
      _enemiesAlive--;
      enemy.OnRelease -= DecreaseAliveEnemies;
    }

    private void SpawnEnemy()
    {
      var enemy = _enemyFactory.GetOrCreateEnemy(GetRandomEnemyType(_currentWave.Enemies),
        _levelSelector.CurrentLevelConfig.EnemyWaypoints[0]);
      enemy.OnRelease += DecreaseAliveEnemies;

      _enemiesAlive++;
      _enemiesLeftToSpawn--;

      _spawnTimer = 0f;
    }

    private EnemyType GetRandomEnemyType(EnemyType[] types) => 
      types[Random.Range(0, types.Length)];
  }
}