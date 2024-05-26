using PortalRift.Runtime.Meta.UI;
using PortalRift.Runtime.Services.Audio;
using PortalRift.Runtime.Services.Configs;
using PortalRift.Runtime.Services.UI;
using VContainer.Unity;

namespace PortalRift.Runtime.Meta
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