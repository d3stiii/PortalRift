using UnityEngine;

namespace PortalRift.Configs
{
  [CreateAssetMenu(menuName = "Configs/Audio", fileName = "AudioConfig")]
  public class AudioConfig : ScriptableObject
  {
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioClip _metaMusicClip;

    public AudioSource SfxSource => _sfxSource;
    public AudioSource MusicSource => _musicSource;
    public AudioClip MetaMusicClip => _metaMusicClip;
  }
}