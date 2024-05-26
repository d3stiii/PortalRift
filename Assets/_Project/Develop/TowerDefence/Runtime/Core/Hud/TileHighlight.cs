using System.Collections;
using TowerDefence.Core.Building;
using TowerDefence.Core.Tiles;
using UnityEngine;
using VContainer;

namespace TowerDefence.Core.Hud
{
  public class TileHighlight : MonoBehaviour
  {
    [SerializeField] private float _speed;
    private BuildingService _buildingService;
    private TileSelection _tileSelection;
    private IEnumerator _movingCoroutine;

    [Inject]
    public void Construct(TileSelection tileSelection, BuildingService buildingService)
    {
      _buildingService = buildingService;
      _tileSelection = tileSelection;
      tileSelection.OnTileSelected += OnTileSelected;
      tileSelection.OnTileUnselected += RemoveHighlight;
      buildingService.OnPlacedTower += RemoveHighlight;
    }

    private void OnTileSelected() => 
      SetHighlight(_tileSelection.TowerHolder.transform.position);

    private void SetHighlight(Vector2 vectorWorldSpace)
    {
      if (_tileSelection.TowerHolder.Tower != null)
        return;

      if (_movingCoroutine != null)
        StopCoroutine(_movingCoroutine);

      var vector = Camera.main.WorldToScreenPoint(vectorWorldSpace);
      _movingCoroutine = SetPosition(vector);

      gameObject.SetActive(true);
      StartCoroutine(_movingCoroutine);
    }

    private void RemoveHighlight()
    {
      if (_movingCoroutine != null)
        StopCoroutine(_movingCoroutine);

      gameObject.SetActive(false);
    }

    private IEnumerator SetPosition(Vector3 position)
    {
      while ((transform.position - position).magnitude > 0.01)
      {
        transform.position = Vector3.Lerp(transform.position, position, _speed * Time.deltaTime);
        yield return null;
      }
    }

    private void OnDestroy()
    {
      _tileSelection.OnTileSelected -= OnTileSelected;
      _tileSelection.OnTileUnselected -= RemoveHighlight;
      _buildingService.OnPlacedTower -= RemoveHighlight;
    }
  }
}