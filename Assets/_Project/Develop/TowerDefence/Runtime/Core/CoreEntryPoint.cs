using System.Linq;
using TowerDefence.Configs;
using TowerDefence.Core.Base;
using TowerDefence.Core.Hud;
using TowerDefence.Core.Levels;
using TowerDefence.Services.Audio;
using TowerDefence.Services.Data;
using TowerDefence.Services.LevelManagement;
using TowerDefence.Services.UI;
using UnityEngine;
using VContainer.Unity;

namespace TowerDefence.Core
{
  public class CoreEntryPoint : IInitializable
  {
    private readonly AudioService _audioService;
    private readonly BaseFactory _baseFactory;
    private readonly LevelFactory _levelFactory;
    private readonly LevelSelector _levelSelector;
    private readonly SessionDataProvider _sessionDataProvider;
    private readonly UIService _uiService;

    public CoreEntryPoint(
      LevelSelector levelSelector,
      LevelFactory levelFactory,
      BaseFactory baseFactory,
      UIService uiService,
      SessionDataProvider sessionDataProvider,
      AudioService audioService)
    {
      _levelSelector = levelSelector;
      _levelFactory = levelFactory;
      _baseFactory = baseFactory;
      _uiService = uiService;
      _sessionDataProvider = sessionDataProvider;
      _audioService = audioService;
    }

    private LevelConfig CurrentLevel => _levelSelector.CurrentLevelConfig;

    public void Initialize()
    {
      _levelFactory.LoadLevel(CurrentLevel);
      var @base = _baseFactory.CreateBase(CurrentLevel.EnemyWaypoints.Last());
      InitializeHud(@base);
      InitializeSessionData();
      _audioService.PlayMusic(CurrentLevel.Music);
    }

    private void InitializeSessionData()
    {
      _sessionDataProvider.SessionData.MoneyData.AddMoney(CurrentLevel.StartMoney);
      _sessionDataProvider.SessionData.WavesData.MaxWave = CurrentLevel.Waves.Length;
    }

    private void InitializeHud(GameObject @base)
    {
      var baseHealth = @base.GetComponent<BaseHealth>();
      _uiService.Hud.GetComponentInChildren<HealthCounter>().Construct(baseHealth);
    }
  }
}