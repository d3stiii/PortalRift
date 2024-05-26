using PortalRift.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PortalRift.Runtime.Core.Building.UI
{
  [RequireComponent(typeof(Button))]
  public class BuildButton : MonoBehaviour
  {
    [SerializeField] private Image _towerImage;
    [SerializeField] private TextMeshProUGUI _costText;
    private BuildingService _buildingService;
    private Button _button;
    private TowerType _towerType;

    public void Construct(BuildingService buildingService, TowerConfig towerConfig)
    {
      _buildingService = buildingService;
      _towerType = towerConfig.Type;
      _costText.text = towerConfig.Cost.ToString();
      _towerImage.sprite = towerConfig.Prefab.GetComponentInChildren<SpriteRenderer>().sprite;
    }

    private void Awake()
    {
      _button = GetComponent<Button>();
      _button.onClick.AddListener(Build);
    }

    private void Build()
    {
      _buildingService.BuildTower(_towerType);
    }

    private void OnDestroy()
    {
      _button.onClick.RemoveListener(Build);
    }
  }
}