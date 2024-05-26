using System;
using System.Collections.Generic;
using System.Linq;
using PortalRift.Runtime.Common.UI;
using PortalRift.Runtime.Core.Hud;
using UnityEngine;

namespace PortalRift.Configs
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