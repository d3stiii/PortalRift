using TowerDefence.Configs;
using UnityEngine;

namespace TowerDefence.Core.Towers
{
  public class Tower : MonoBehaviour
  {
    [SerializeField] private TowerType _type;

    public TowerType Type => _type;
  }
}