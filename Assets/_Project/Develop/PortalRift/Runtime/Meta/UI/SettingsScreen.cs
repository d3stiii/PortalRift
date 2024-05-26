using PortalRift.Runtime.Common.UI;
using PortalRift.Runtime.Data.Persistent;
using PortalRift.Runtime.Services.Data;
using PortalRift.Runtime.Services.SaveLoad;
using PortalRift.Runtime.Services.UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace PortalRift.Runtime.Meta.UI
{
  public class SettingsScreen : BaseWindow
  {
    [SerializeField] private Button _closeButton;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _sfxVolume;
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private Toggle _sfxToggle;
    private SaveLoadService _saveLoadService;
    private SoundSettingsData _soundSettings;
    private UIService _uiService;

    [Inject]
    public void Construct(UIService uiService, PersistentDataProvider persistentDataProvider,
      SaveLoadService saveLoadService)
    {
      _saveLoadService = saveLoadService;
      _soundSettings = persistentDataProvider.PersistentData.SoundSettingsData;
      _uiService = uiService;
    }

    protected override void Initialize()
    {
      _musicVolumeSlider.value = _soundSettings.MusicVolume;
      _sfxVolume.value = _soundSettings.SfxVolume;
      _sfxToggle.isOn = _soundSettings.SfxEnabled;
      _musicToggle.isOn = _soundSettings.MusicEnabled;

      _closeButton.onClick.AddListener(ReturnHome);
      _musicVolumeSlider.onValueChanged.AddListener(ChangeMusicVolume);
      _sfxVolume.onValueChanged.AddListener(ChangeSfxVolume);
      _musicToggle.onValueChanged.AddListener(ToggleMusic);
      _sfxToggle.onValueChanged.AddListener(ToggleSfx);
    }

    protected override void Cleanup()
    {
      _closeButton.onClick.RemoveListener(ReturnHome);
      _musicVolumeSlider.onValueChanged.RemoveListener(ChangeMusicVolume);
      _sfxVolume.onValueChanged.RemoveListener(ChangeSfxVolume);
      _musicToggle.onValueChanged.RemoveListener(ToggleMusic);
      _sfxToggle.onValueChanged.RemoveListener(ToggleSfx);
    }

    private void ToggleMusic(bool value)
    {
      if (!value)
        _musicVolumeSlider.value = 0;
      
      _soundSettings.MusicEnabled = value;
    }

    private void ToggleSfx(bool value)
    {
      if (!value)
        _sfxVolume.value = 0;

      _soundSettings.SfxEnabled = value;
    }

    private void ChangeSfxVolume(float value)
    {
      _sfxToggle.isOn = value > 0;
      _soundSettings.SetSfxVolume(value);
    }

    private void ChangeMusicVolume(float value)
    {
      _musicToggle.isOn = value > 0;
      _soundSettings.SetMusicVolume(value);
    }

    private void ReturnHome()
    {
      _uiService.Close<SettingsScreen>();
      _saveLoadService.Save();
    }
  }
}