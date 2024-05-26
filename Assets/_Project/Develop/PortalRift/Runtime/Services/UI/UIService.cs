using System;
using System.Collections.Generic;
using PortalRift.Runtime.Common.UI;
using PortalRift.Runtime.Core.Hud;

namespace PortalRift.Runtime.Services.UI
{
  public class UIService
  {
    private readonly Dictionary<Type, BaseWindow> _cachedWindows = new();
    private readonly UIFactory _uiFactory;
    private Hud _hud;

    public UIService(UIFactory uiFactory) => 
      _uiFactory = uiFactory;

    public Hud Hud => _hud ??= _uiFactory.CreateHud();

    public TWindow Open<TWindow>() where TWindow : BaseWindow
    {
      if (_cachedWindows.TryGetValue(typeof(TWindow), out var cachedWindow))
      {
        cachedWindow.gameObject.SetActive(true);
        return (TWindow)cachedWindow;
      }

      var newWindow = _uiFactory.CreateWindow<TWindow>();
      _cachedWindows.Add(typeof(TWindow), newWindow);

      return (TWindow)newWindow;
    }

    public void Close<TWindow>() where TWindow : BaseWindow
    {
      if (_cachedWindows.TryGetValue(typeof(TWindow), out var cachedWindow))
        cachedWindow.gameObject.SetActive(false);
    }

    public void CloseAll()
    {
      foreach (var openedWindow in _cachedWindows.Values)
        openedWindow.gameObject.SetActive(false);
    }
  }
}