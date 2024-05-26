using UnityEngine;

namespace PortalRift.Configs
{
  [CreateAssetMenu(menuName = "Configs/Base", fileName = "BaseConfig")]
  public class BaseConfig : ScriptableObject
  {
    [SerializeField] private int _health;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private AudioClip _damageSound;

    public int Health => _health;
    public GameObject Prefab => _prefab;
    public AudioClip DamageSound => _damageSound;
  }
}