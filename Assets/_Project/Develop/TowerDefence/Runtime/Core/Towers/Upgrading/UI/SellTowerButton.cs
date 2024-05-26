using TowerDefence.Core.Tiles;
using TowerDefence.Data.Session;
using TowerDefence.Services.Data;
using TowerDefence.Services.UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace TowerDefence.Core.Towers.Upgrading.UI
{
  public class SellTowerButton : MonoBehaviour
  {
    [SerializeField] private Button _sellButton;
    private MoneyData _moneyData;
    private TileSelection _tileSelection;
    private UIService _uiService;

    [Inject]
    public void Construct(UIService uiService, TileSelection tileSelection, SessionDataProvider sessionDataProvider)
    {
      _uiService = uiService;
      _tileSelection = tileSelection;
      _moneyData = sessionDataProvider.SessionData.MoneyData;
    }

    private void OnEnable() =>
      _sellButton.onClick.AddListener(Sell);

    private void Sell()
    {
      var tower = _tileSelection.TowerHolder.Tower;
      _moneyData.AddMoney((int)(tower.GetComponent<TowerUpgrading>().MoneySpent * 0.8f));
      _tileSelection.TowerHolder.RemoveTower();
      _uiService.CloseAll();
      _tileSelection.Unselect();
    }

    private void OnDisable() =>
      _sellButton.onClick.RemoveListener(Sell);
  }
}