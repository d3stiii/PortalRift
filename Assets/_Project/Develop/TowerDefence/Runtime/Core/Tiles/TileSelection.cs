using System;

namespace TowerDefence.Core.Tiles
{
  public class TileSelection
  {
    public event Action OnTileSelected;
    public event Action OnTileUnselected;
    public TowerHolder TowerHolder { get; private set; }

    public void Select(TowerHolder holder)
    {
      Unselect();
      TowerHolder = holder;
      OnTileSelected?.Invoke();
    }

    public void Unselect()
    {
      TowerHolder = null;
      OnTileUnselected?.Invoke();
    }
  }
}