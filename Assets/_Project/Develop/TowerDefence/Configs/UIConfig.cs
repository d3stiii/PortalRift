using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Common.UI;
using TowerDefence.Core.Hud;
using UnityEngine;

namespace TowerDefence.Configs
{
  [CreateAssetMenu(menuName = "Configs/UIConfig", fileName = "UIConfig")]
  public class UIConfig : ScriptableObject
  {
    [SerializeField] private List<BaseWindow> _windows;
    [SerializeField] private RectTransform _uiRootPrefab;
    [SerializeField] private Hud _hudPrefab;
    public RectTransform UIRootPrefab => _uiRootPrefab;
    public Hud HudPrefab => _hudPrefab;

    public TScreen GetScreen<TScreen>() where TScreen : BaseWindow =>
      _windows.OfType<TScreen>().FirstOrDefault() ??
      throw new IndexOutOfRangeException($"Can't find window of type {typeof(TScreen)}");
  }
}