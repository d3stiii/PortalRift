using PortalRift.Runtime.Data.Persistent;
using PortalRift.Runtime.Services.Configs;
using PortalRift.Runtime.Services.Data;
using UnityEngine;

namespace PortalRift.Runtime.Services.Audio
{
  public class AudioService
  {
    private readonly SoundSettingsData _soundSettings;
    private AudioSource _musicSource;
    private AudioSource _sfxSource;

    public AudioService(PersistentDataProvider persistentDataProvider, ConfigProvider configProvider)
    {
      _soundSettings = persistentDataProvider.PersistentData.SoundSettingsData;

      InitializeSources(configProvider);

      _soundSettings.Changed += UpdateSourcesSettings;
    }

    private void InitializeSources(ConfigProvider configProvider)
    {
      _sfxSource = Object.Instantiate(configProvider.AudioConfig.SfxSource);
      _musicSource = Object.Instantiate(configProvider.AudioConfig.MusicSource);
      Object.DontDestroyOnLoad(_sfxSource);
      Object.DontDestroyOnLoad(_musicSource);

      UpdateSourcesSettings();
    }

    private void UpdateSourcesSettings()
    {
      _sfxSource.mute = !_soundSettings.SfxEnabled;
      _sfxSource.volume = _soundSettings.SfxVolume;
      _musicSource.mute = !_soundSettings.MusicEnabled;
      _musicSource.volume = _soundSettings.MusicVolume;
      _musicSource.loop = true;
    }

    public void PlaySfx(AudioClip audioClip)
    {
      _sfxSource.PlayOneShot(audioClip);
    }

    public void PlayMusic(AudioClip audioClip)
    {
      _musicSource.clip = audioClip;
      _musicSource.Play();
    }
  }
}