using PortalRift.Configs;
using PortalRift.Runtime.Common.Extensions;
using PortalRift.Runtime.Services.Configs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace PortalRift.Runtime.Core.Towers
{
  public class TowerFactory
  {
    private readonly ConfigProvider _configProvider;
    private readonly IObjectResolver _container;

    public TowerFactory(IObjectResolver container, ConfigProvider configProvider)
    {
      _container = container;
      _configProvider = configProvider;
    }

    public Tower CreateTower(TowerType towerType, Transform parent)
    {
      var towerConfig = _configProvider.GetTowerConfig(towerType);
      var tower = _container.Instantiate(towerConfig.Prefab, parent.position, parent.rotation, parent)
        .With(tower => tower.GetComponent<TowerAttack>().Configure(towerConfig))
        .With(tower => tower.GetComponent<TowerUpgrading>().Construct(towerConfig));
      return tower;
    }
  }
}