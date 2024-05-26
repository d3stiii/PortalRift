using TowerDefence.Services.Configs;
using TowerDefence.Services.SaveLoad;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace TowerDefence.Bootstrap
{
  public class BootstrapEntryPoint : IInitializable
  {
    private readonly ConfigProvider _configProvider;
    private readonly SaveLoadService _saveLoadService;

    public BootstrapEntryPoint(ConfigProvider configProvider, SaveLoadService saveLoadService)
    {
      _configProvider = configProvider;
      _saveLoadService = saveLoadService;
    }

    public void Initialize()
    {
      _configProvider.Load();
      _saveLoadService.Load();
      SceneManager.LoadScene("Meta");
    }
  }
}