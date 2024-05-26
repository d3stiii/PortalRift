using TowerDefence.Common;
using TowerDefence.Services.AssetsManagement;
using TowerDefence.Services.Audio;
using TowerDefence.Services.Configs;
using TowerDefence.Services.Data;
using TowerDefence.Services.LevelManagement;
using TowerDefence.Services.SaveLoad;
using VContainer;
using VContainer.Unity;

namespace TowerDefence.Bootstrap
{
  public class ProjectScope : LifetimeScope
  {
    protected override void Configure(IContainerBuilder builder)
    {
      BindCommonServices(builder);
      BindLevelManagement(builder);
      BindSoundManagement(builder);
      BindData(builder);
    }

    private static void BindCommonServices(IContainerBuilder builder) =>
      builder.RegisterComponentOnNewGameObject<CoroutineRunner>(Lifetime.Singleton).As<ICoroutineRunner>();

    private static void BindLevelManagement(IContainerBuilder builder) =>
      builder.Register<LevelSelector>(Lifetime.Singleton);

    private static void BindSoundManagement(IContainerBuilder builder) =>
      builder.Register<AudioService>(Lifetime.Singleton);

    private static void BindData(IContainerBuilder builder)
    {
      builder.Register<AssetLoader>(Lifetime.Singleton);
      builder.Register<ConfigProvider>(Lifetime.Singleton);
      builder.Register<PersistentDataProvider>(Lifetime.Singleton);
      builder.Register<SaveLoadService>(Lifetime.Singleton);
    }
  }
}