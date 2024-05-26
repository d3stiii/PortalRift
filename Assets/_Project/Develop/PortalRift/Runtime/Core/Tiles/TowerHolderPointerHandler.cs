using PortalRift.Runtime.Core.Building.UI;
using PortalRift.Runtime.Core.Pause;
using PortalRift.Runtime.Core.Towers.Upgrading.UI;
using PortalRift.Runtime.Services.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace PortalRift.Runtime.Core.Tiles
{
  [RequireComponent(typeof(TowerHolder))]
  public class TowerHolderPointerHandler : MonoBehaviour, IPointerDownHandler
  {
    private PauseService _pauseService;
    private TileSelection _tileSelection;
    private TowerHolder _towerHolder;
    private UIService _uiService;

    [Inject]
    public void Construct(UIService uiService, TileSelection tileSelection, PauseService pauseService)
    {
      _pauseService = pauseService;
      _tileSelection = tileSelection;
      _uiService = uiService;
    }

    private void Awake() => 
      _towerHolder = GetComponent<TowerHolder>();

    public void OnPointerDown(PointerEventData eventData)
    {
      if (_pauseService.IsPaused)
        return;

      CloseAllTowerWindows();

      if (_tileSelection.TowerHolder == _towerHolder)
      {
        _tileSelection.Unselect();
        return;
      }

      _tileSelection.Select(_towerHolder);

      if (_towerHolder.Tower == null)
        _uiService.Open<BuildingWindow>();
      else
        _uiService.Open<UpgradingWindow>();
    }

    private void CloseAllTowerWindows()
    {
      _uiService.Close<BuildingWindow>();
      _uiService.Close<UpgradingWindow>();
    }
  }
}