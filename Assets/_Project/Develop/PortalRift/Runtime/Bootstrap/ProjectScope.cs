using PortalRift.Runtime.Common;
using PortalRift.Runtime.Services.AssetsManagement;
using PortalRift.Runtime.Services.Audio;
using PortalRift.Runtime.Services.Configs;
using PortalRift.Runtime.Services.Data;
using PortalRift.Runtime.Services.LevelManagement;
using PortalRift.Runtime.Services.SaveLoad;
using VContainer;
using VContainer.Unity;

namespace PortalRift.Runtime.Bootstrap
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