using TowerDefence.Core.Building;
using TowerDefence.Core.Tiles;
using TowerDefence.Core.Towers;
using TowerDefence.Core.Towers.Upgrading;
using UnityEngine;
using VContainer;

namespace TowerDefence.Core.Hud
{
  public class TowerRangeView : MonoBehaviour
  {
    private BuildingService _buildingService;
    private TileSelection _tileSelection;
    private UpgradingService _upgradingService;

    [Inject]
    public void Construct(TileSelection tileSelection, BuildingService buildingService,
      UpgradingService upgradingService)
    {
      _upgradingService = upgradingService;
      _buildingService = buildingService;
      _tileSelection = tileSelection;
      _tileSelection.OnTileSelected += ShowTowerRange;
      _tileSelection.OnTileUnselected += HideTowerRange;
      _buildingService.OnPlacedTower += ShowTowerRange;
      _upgradingService.OnUpgradedTower += ShowTowerRange;
    }

    private void ShowTowerRange()
    {
      var tower = _tileSelection.TowerHolder.Tower;
      if (tower == null)
        return;

      var towerRange = tower.GetComponent<TowerUpgrading>().CurrentUpgradeData.Stats.Radius;
      transform.position = Camera.main.WorldToScreenPoint(tower.transform.position);
      transform.localScale = new Vector3(towerRange * 2.15f, towerRange * 2.15f, 1f);

      gameObject.SetActive(true);
    }

    private void HideTowerRange() => 
      gameObject.SetActive(false);

    private void OnDestroy()
    {
      _tileSelection.OnTileSelected -= ShowTowerRange;
      _tileSelection.OnTileUnselected -= HideTowerRange;
      _buildingService.OnPlacedTower -= ShowTowerRange;
      _upgradingService.OnUpgradedTower -= ShowTowerRange;
    }
  }
}