﻿using System;
using TowerDefence.Configs;
using TowerDefence.Core.Pause;
using TowerDefence.Core.Tiles;
using TowerDefence.Data.Session;
using TowerDefence.Services.Configs;
using TowerDefence.Services.Data;

namespace TowerDefence.Core.Towers.Upgrading
{
  public class UpgradingService
  {
    public event Action OnUpgradedTower;
    
    private readonly ConfigProvider _configProvider;
    private readonly MoneyData _moneyData;
    private readonly PauseService _pauseService;
    private readonly TileSelection _tileSelection;
    
    public UpgradingService(
      SessionDataProvider sessionDataProvider,
      TileSelection tileSelection,
      ConfigProvider configProvider,
      PauseService pauseService)
    {
      _tileSelection = tileSelection;
      _configProvider = configProvider;
      _pauseService = pauseService;
      _moneyData = sessionDataProvider.SessionData.MoneyData;
    }

    public void UpgradeTower()
    {
      var selectedTower = _tileSelection.TowerHolder.Tower;
      if (selectedTower == null || !TryGetNextLevel(out _) || _pauseService.IsPaused)
        return;

      var towerUpgrading = selectedTower.GetComponent<TowerUpgrading>();

      var upgradingCost = towerUpgrading.CurrentUpgradeData.Cost;
      if (upgradingCost > _moneyData.Value)
        return;

      _moneyData.SpendMoney(upgradingCost);
      towerUpgrading.Upgrade();
      OnUpgradedTower?.Invoke();
    }

    public bool TryGetNextLevel(out UpgradeLevel upgradeLevel)
    {
      upgradeLevel = null;

      var selectedTower = _tileSelection.TowerHolder.Tower;
      if (selectedTower == null)
        return false;

      var nextLevel = selectedTower.GetComponent<TowerUpgrading>().CurrentUpgradeLevel + 1;
      var towerConfig = _configProvider.GetTowerConfig(selectedTower.Type);
      if (nextLevel >= towerConfig.UpgradeLevels.Count)
        return false;

      upgradeLevel = towerConfig.UpgradeLevels[nextLevel];
      return upgradeLevel != null;
    }
  }
}