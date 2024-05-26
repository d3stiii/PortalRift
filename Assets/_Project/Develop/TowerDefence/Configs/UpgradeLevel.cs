using System;
using UnityEngine;

namespace TowerDefence.Configs
{
  [Serializable]
  public class UpgradeLevel
  {
    public Stats Stats;
    public Sprite Sprite;
    public int Cost;
  }
}