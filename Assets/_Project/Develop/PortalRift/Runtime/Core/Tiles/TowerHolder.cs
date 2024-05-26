using PortalRift.Configs;
using PortalRift.Runtime.Core.Towers;
using UnityEngine;
using VContainer;

namespace PortalRift.Runtime.Core.Tiles
{
  public class TowerHolder : MonoBehaviour
  {
    private TowerFactory _towerFactory;
    public Tower Tower { get; private set; }

    [Inject]
    public void Construct(TowerFactory towerFactory) => 
      _towerFactory = towerFactory;

    public void SetTower(TowerType towerType) => 
      Tower = _towerFactory.CreateTower(towerType, transform);

    public void RemoveTower() => 
      Destroy(Tower.gameObject);
  }
}