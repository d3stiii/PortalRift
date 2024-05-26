using System;
using TowerDefence.Configs;
using TowerDefence.Core.Pause;
using TowerDefence.Core.Tiles;
using TowerDefence.Data.Session;
using TowerDefence.Services.Configs;
using TowerDefence.Services.Data;

namespace TowerDefence.Core.Building
{
  public class BuildingService
  {
    public event Action OnPlacedTower;
    
    private readonly ConfigProvider _configProvider;
    private readonly MoneyData _moneyData;
    private readonly PauseService _pauseService;
    private readonly TileSelection _tileSelection;
    
    public BuildingService(
      ConfigProvider configProvider,
      SessionDataProvider sessionDataProvider,
      TileSelection tileSelection,
      PauseService pauseService)
    {
      _moneyData = sessionDataProvider.SessionData.MoneyData;
      _configProvider = configProvider;
      _tileSelection = tileSelection;
      _pauseService = pauseService;
    }

    public void BuildTower(TowerType towerType)
    {
      if (_pauseService.IsPaused)
        return;

      var cost = _configProvider.GetTowerConfig(towerType).Cost;

      if (_tileSelection.TowerHolder.Tower != null || cost > _moneyData.Value)
        return;

      _tileSelection.TowerHolder.SetTower(towerType);
      _moneyData.SpendMoney(cost);

      OnPlacedTower?.Invoke();
    }
  }
}