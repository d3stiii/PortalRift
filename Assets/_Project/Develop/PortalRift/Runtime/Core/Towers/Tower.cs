using PortalRift.Configs;
using UnityEngine;

namespace PortalRift.Runtime.Core.Towers
{
  public class Tower : MonoBehaviour
  {
    [SerializeField] private TowerType _type;

    public TowerType Type => _type;
  }
}