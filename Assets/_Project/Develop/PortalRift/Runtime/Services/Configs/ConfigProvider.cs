using System.Collections.Generic;
using System.Linq;
using PortalRift.Configs;
using PortalRift.Runtime.Core.Projectiles;
using PortalRift.Runtime.Services.AssetsManagement;

namespace PortalRift.Runtime.Services.Configs
{
  public class ConfigProvider
  {
    private readonly AssetLoader _assetLoader;
    private Dictionary<EnemyType, EnemyConfig> _enemies;
    private Dictionary<ProjectileType, ProjectileConfig> _projectiles;
    private Dictionary<TowerType, TowerConfig> _towers;

    public ConfigProvider(AssetLoader assetLoader) => 
      _assetLoader = assetLoader;

    public UIConfig UIConfig { get; private set; }
    public LevelConfig[] Levels { get; private set; }

    public BaseConfig BaseConfig { get; private set; }
    public AudioConfig AudioConfig { get; private set; }

    public EnemyConfig GetEnemyConfig(EnemyType type) => 
      _enemies.GetValueOrDefault(type);

    public TowerConfig GetTowerConfig(TowerType type) => 
      _towers.GetValueOrDefault(type);

    public ProjectileConfig GetProjectileConfig(ProjectileType type) => 
      _projectiles.GetValueOrDefault(type);

    public void Load()
    {
      UIConfig = _assetLoader.Load<UIConfig>(AssetsPath.UIConfigPath);
      Levels = _assetLoader.LoadAll<LevelConfig>(AssetsPath.LevelsDataPath);
      BaseConfig = _assetLoader.Load<BaseConfig>(AssetsPath.BaseConfig);
      AudioConfig = _assetLoader.Load<AudioConfig>(AssetsPath.AudioConfig);

      _projectiles = _assetLoader
        .LoadAll<ProjectileConfig>(AssetsPath.ProjectileConfigsPath)
        .ToDictionary(x => x.Type, x => x);

      _enemies = _assetLoader
        .LoadAll<EnemyConfig>(AssetsPath.EnemyConfigsPath)
        .ToDictionary(x => x.Type, x => x);

      _towers = _assetLoader
        .LoadAll<TowerConfig>(AssetsPath.TowerConfigsPath)
        .ToDictionary(x => x.Type, x => x);
    }
  }
}