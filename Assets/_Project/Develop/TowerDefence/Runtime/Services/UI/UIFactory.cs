using TowerDefence.Common.UI;
using TowerDefence.Core.Hud;
using TowerDefence.Services.Configs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace TowerDefence.Services.UI
{
  public class UIFactory
  {
    private readonly ConfigProvider _configProvider;
    private readonly IObjectResolver _container;
    private RectTransform _uiRoot;

    public UIFactory(ConfigProvider configProvider, IObjectResolver container)
    {
      _configProvider = configProvider;
      _container = container;
    }

    public BaseWindow CreateWindow<TWindow>() where TWindow : BaseWindow
    {
      var screenPrefab = _configProvider.UIConfig.GetScreen<TWindow>();
      return _container.Instantiate(screenPrefab, GetOrCreateUIRoot());
    }

    public Hud CreateHud()
    {
      return _container.Instantiate(_configProvider.UIConfig.HudPrefab, GetOrCreateUIRoot());
    }

    private RectTransform GetOrCreateUIRoot()
    {
      if (_uiRoot == null)
        _uiRoot = Object.Instantiate(_configProvider.UIConfig.UIRootPrefab);

      return _uiRoot;
    }
  }
}