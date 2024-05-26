using PortalRift.Configs;
using UnityEngine;

namespace PortalRift.Runtime.Core.Towers
{
  public class TowerUpgrading : MonoBehaviour
  {
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private TowerConfig _config;

    public int CurrentUpgradeLevel { get; private set; }
    public int MoneySpent { get; private set; }
    public UpgradeLevel CurrentUpgradeData => _config.UpgradeLevels[CurrentUpgradeLevel];

    public void Construct(TowerConfig config)
    {
      _config = config;
      MoneySpent = config.Cost;
      _spriteRenderer.sprite = CurrentUpgradeData.Sprite;
    }

    public void Upgrade()
    {
      MoneySpent += CurrentUpgradeData.Cost;
      CurrentUpgradeLevel++;
      _spriteRenderer.sprite = CurrentUpgradeData.Sprite;
    }
  }
}