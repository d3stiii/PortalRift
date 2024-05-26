using TowerDefence.Core.Base;
using TowerDefence.Core.Building;
using TowerDefence.Core.Enemies;
using TowerDefence.Core.Levels;
using TowerDefence.Core.Pause;
using TowerDefence.Core.Projectiles;
using TowerDefence.Core.Results;
using TowerDefence.Core.Tiles;
using TowerDefence.Core.Towers;
using TowerDefence.Core.Towers.Upgrading;
using TowerDefence.Core.Waves;
using TowerDefence.Core.Waves.States;
using TowerDefence.Services.Data;
using TowerDefence.Services.UI;
using VContainer;
using VContainer.Unity;

namespace TowerDefence.Core
{
  public class CoreScope : LifetimeScope
  {
    protected override void Configure(IContainerBuilder builder)
    {
      builder.RegisterEntryPoint<CoreEntryPoint>();

      BindUI(builder);
      BindWaveSystem(builder);
      BindFactories(builder);
      BindTilesManagement(builder);
      BindGameManagement(builder);
      BindData(builder);
    }

    private static void BindUI(IContainerBuilder builder)
    {
      builder.Register<UIFactory>(Lifetime.Singleton);
      builder.Register<UIService>(Lifetime.Singleton);
    }

    private static void BindWaveSystem(IContainerBuilder builder)
    {
      builder.Register<WaveStateMachine>(Lifetime.Singleton);
      builder.Register<StateFactory>(Lifetime.Singleton);
      builder.Register<WaitForWaveState>(Lifetime.Singleton);
      builder.Register<AttackState>(Lifetime.Singleton);
      builder.Register<CheckWinState>(Lifetime.Singleton);
    }

    private static void BindFactories(IContainerBuilder builder)
    {
      builder.Register<LevelFactory>(Lifetime.Singleton);
      builder.Register<BaseFactory>(Lifetime.Singleton);
      builder.Register<EnemyFactory>(Lifetime.Singleton);
      builder.Register<TowerFactory>(Lifetime.Singleton);
      builder.Register<ProjectileFactory>(Lifetime.Singleton);
    }

    private static void BindTilesManagement(IContainerBuilder builder)
    {
      builder.Register<BuildingService>(Lifetime.Singleton);
      builder.Register<UpgradingService>(Lifetime.Singleton);
      builder.Register<TileSelection>(Lifetime.Singleton);
    }

    private static void BindGameManagement(IContainerBuilder builder)
    {
      builder.Register<GameResult>(Lifetime.Singleton);
      builder.Register<PauseService>(Lifetime.Singleton);
    }

    private static void BindData(IContainerBuilder builder) => 
      builder.Register<SessionDataProvider>(Lifetime.Singleton);
  }
}