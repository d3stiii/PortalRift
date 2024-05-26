using TowerDefence.Meta.UI;
using TowerDefence.Services.Audio;
using TowerDefence.Services.Configs;
using TowerDefence.Services.UI;
using VContainer.Unity;

namespace TowerDefence.Meta
{
  public class MetaEntryPoint : IInitializable
  {
    private readonly AudioService _audioService;
    private readonly ConfigProvider _configProvider;
    private readonly UIService _uiService;

    public MetaEntryPoint(UIService uiService, AudioService audioService, ConfigProvider configProvider)
    {
      _uiService = uiService;
      _audioService = audioService;
      _configProvider = configProvider;
    }

    public void Initialize()
    {
      _uiService.Open<HomeScreen>();
      _audioService.PlayMusic(_configProvider.AudioConfig.MetaMusicClip);
    }
  }
}