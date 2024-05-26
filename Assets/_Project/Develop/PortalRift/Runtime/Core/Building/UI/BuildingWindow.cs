using System;
using PortalRift.Configs;
using PortalRift.Runtime.Common.Extensions;
using PortalRift.Runtime.Common.UI;
using PortalRift.Runtime.Core.Tiles;
using PortalRift.Runtime.Core.Towers.Upgrading.UI;
using PortalRift.Runtime.Services.Configs;
using PortalRift.Runtime.Services.UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace PortalRift.Runtime.Core.Building.UI
{
  public class BuildingWindow : BaseWindow
  {
    [SerializeField] private Button _closeButton;
    [SerializeField] private RectTransform _towerButtonsContainer;
    [SerializeField] private BuildButton _buildButtonPrefab;
    private BuildingService _buildingService;
    private ConfigProvider _configProvider;
    private TileSelection _tileSelection;
    private UIService _uiService;

    [Inject]
    public void Construct(UIService uiService,
      BuildingService buildingService,
      TileSelection tileSelection,
      ConfigProvider configProvider)
    {
      _configProvider = configProvider;
      _tileSelection = tileSelection;
      _buildingService = buildingService;
      _uiService = uiService;
    }

    protected override void Initialize()
    {
      _buildingService.OnPlacedTower += ShowUpgradingWindow;
      _closeButton.onClick.AddListener(Close);
      _closeButton.onClick.AddListener(_tileSelection.Unselect);

      GenerateTowerButtons();
    }

    private void GenerateTowerButtons()
    {
      foreach (var towerType in Enum.GetValues(typeof(TowerType)))
      {
        var towerConfig = _configProvider.GetTowerConfig((TowerType)towerType);
        Instantiate(_buildButtonPrefab, _towerButtonsContainer)
          .With(x => x.Construct(_buildingService, towerConfig));
      }
    }

    protected override void Cleanup()
    {
      _buildingService.OnPlacedTower -= ShowUpgradingWindow;
      _closeButton.onClick.RemoveListener(Close);
      _closeButton.onClick.RemoveListener(_tileSelection.Unselect);
    }

    private void ShowUpgradingWindow()
    {
      Close();
      _uiService.Open<UpgradingWindow>();
    }

    private void Close() =>
      _uiService.Close<BuildingWindow>();
  }
}