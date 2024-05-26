using System;
using UnityEngine;

namespace PortalRift.Runtime.Data.Persistent
{
  [Serializable]
  public class SoundSettingsData
  {
    public event Action Changed;
    
    public float MusicVolume;
    public float SfxVolume;
    public bool MusicEnabled;
    public bool SfxEnabled;

    public void SetMusicVolume(float value)
    {
      MusicVolume = Mathf.Clamp(value, 0, 1);
      Changed?.Invoke();
    }

    public void SetSfxVolume(float value)
    {
      SfxVolume = Mathf.Clamp(value, 0, 1);
      Changed?.Invoke();
    }
  }
}