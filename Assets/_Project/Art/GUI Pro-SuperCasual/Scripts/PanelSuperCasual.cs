using UnityEngine;

namespace LayerLab.SuperCasual
{
  public class PanelSuperCasual : MonoBehaviour
  {
    [SerializeField] private GameObject[] otherPanels;

    public void OnEnable()
    {
      for (var i = 0; i < otherPanels.Length; i++) otherPanels[i].SetActive(true);
    }

    public void OnDisable()
    {
      for (var i = 0; i < otherPanels.Length; i++) otherPanels[i].SetActive(false);
    }
  }
}