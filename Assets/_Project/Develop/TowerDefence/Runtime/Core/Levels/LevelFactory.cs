using TowerDefence.Configs;
using TowerDefence.Core.Pause;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace TowerDefence.Core.Levels
{
  public class LevelFactory
  {
    private readonly IObjectResolver _objectResolver;
    private readonly PauseService _pauseService;

    public LevelFactory(IObjectResolver objectResolver, PauseService pauseService)
    {
      _objectResolver = objectResolver;
      _pauseService = pauseService;
    }

    public Level LoadLevel(LevelConfig level)
    {
      var levelInstance = _objectResolver.Instantiate(level.Prefab, Vector3.zero, Quaternion.identity);

      foreach (var handler in levelInstance.GetComponentsInChildren<IPauseHandler>())
        _pauseService.RegisterHandler(handler);

      return levelInstance;
    }
  }
}