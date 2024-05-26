using PortalRift.Runtime.Core.Base;
using PortalRift.Runtime.Core.Building;
using PortalRift.Runtime.Core.Enemies;
using PortalRift.Runtime.Core.Levels;
using PortalRift.Runtime.Core.Pause;
using PortalRift.Runtime.Core.Projectiles;
using PortalRift.Runtime.Core.Results;
using PortalRift.Runtime.Core.Tiles;
using PortalRift.Runtime.Core.Towers;
using PortalRift.Runtime.Core.Towers.Upgrading;
using PortalRift.Runtime.Core.Waves;
using PortalRift.Runtime.Core.Waves.States;
using PortalRift.Runtime.Services.Data;
using PortalRift.Runtime.Services.UI;
using VContainer;
using VContainer.Unity;

namespace PortalRift.Runtime.Core
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