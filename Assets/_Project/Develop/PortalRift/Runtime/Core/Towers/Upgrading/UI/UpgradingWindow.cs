using PortalRift.Configs;
using PortalRift.Runtime.Common.UI;
using PortalRift.Runtime.Core.Tiles;
using PortalRift.Runtime.Services.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace PortalRift.Runtime.Core.Towers.Upgrading.UI
{
  public class UpgradingWindow : BaseWindow
  {
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _upgradeButton;

    [Space] 
    [Header("Parameters")]
    [SerializeField] private TextMeshProUGUI _cost;
    [SerializeField] private TextMeshProUGUI _currentDamage;
    [SerializeField] private TextMeshProUGUI _damageIncrement;
    [SerializeField] private TextMeshProUGUI _currentCooldown;
    [SerializeField] private TextMeshProUGUI _cooldownDecrement;
    [SerializeField] private TextMeshProUGUI _currentRadius;
    [SerializeField] private TextMeshProUGUI _radiusIncrement;
    [SerializeField] private TextMeshProUGUI _sellCost;
    private TileSelection _tileSelection;
    private UpgradingService _upgradingService;
    private UIService _windowService;

    [Inject]
    public void Construct(UIService uiService, UpgradingService upgradingService, TileSelection tileSelection)
    {
      _tileSelection = tileSelection;
      _windowService = uiService;
      _upgradingService = upgradingService;
    }

    protected override void Initialize()
    {
      _closeButton.onClick.AddListener(Close);
      _upgradeButton.onClick.AddListener(UpgradeTower);
      _upgradingService.OnUpgradedTower += UpdateStatsView;
    }

    protected override void OnOpening() => 
      UpdateStatsView();

    protected override void Cleanup()
    {
      _closeButton.onClick.RemoveListener(Close);
      _upgradeButton.onClick.RemoveListener(UpgradeTower);
      _upgradingService.OnUpgradedTower -= UpdateStatsView;
    }

    private void UpdateStatsView()
    {
      var tower = _tileSelection.TowerHolder.Tower;
      if (tower == null)
        return;
      
      var towerUpgrading = tower.GetComponent<TowerUpgrading>();
      DisplayCurrentStats(towerUpgrading.CurrentUpgradeData);
      DisplayUpgradeAvailability();
      DisplayUpgradeDetails(towerUpgrading.CurrentUpgradeData);
      DisplaySellInfo(towerUpgrading);
    }

    private void UpgradeTower() => 
      _upgradingService.UpgradeTower();

    private void DisplaySellInfo(TowerUpgrading towerUpgrading) => 
      _sellCost.text = $"Sell for {towerUpgrading.MoneySpent * 0.8}";

    private void DisplayCurrentStats(UpgradeLevel upgradeData)
    {
      var currentStats = upgradeData.Stats;

      _currentDamage.text = $"Damage: {currentStats.Damage}";
      _currentCooldown.text = $"Cooldown: {currentStats.Cooldown:0.00}";
      _currentRadius.text = $"Radius: {currentStats.Radius}";
    }

    private void DisplayUpgradeAvailability()
    {
      var nextLevelAvailable = _upgradingService.TryGetNextLevel(out var nextLevel);
      _upgradeButton.gameObject.SetActive(nextLevelAvailable);
      _damageIncrement.gameObject.SetActive(nextLevelAvailable);
      _radiusIncrement.gameObject.SetActive(nextLevelAvailable);
      _cooldownDecrement.gameObject.SetActive(nextLevelAvailable);
    }

    private void DisplayUpgradeDetails(UpgradeLevel upgradeData)
    {
      if (!_upgradingService.TryGetNextLevel(out var nextLevel))
      {
        _cost.text = "MAX LEVEL";
        return;
      }

      var currentStats = upgradeData.Stats;
      _cost.text = $"Cost {upgradeData.Cost}";
      _damageIncrement.text = $"+{nextLevel.Stats.Damage - currentStats.Damage:0.00}";
      _radiusIncrement.text = $"+{nextLevel.Stats.Radius - currentStats.Radius:0.000}";
      _cooldownDecrement.text = $"-{currentStats.Cooldown - nextLevel.Stats.Cooldown:0.00}";
    }

    private void Close()
    {
      _tileSelection.Unselect();
      _windowService.Close<UpgradingWindow>();
    }
  }
}