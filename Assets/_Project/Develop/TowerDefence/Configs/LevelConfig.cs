using System.Collections.Generic;
using TowerDefence.Core.Levels;
using UnityEngine;

namespace TowerDefence.Configs
{
  [CreateAssetMenu(menuName = "Configs/LevelConfig", fileName = "LevelConfig")]
  public class LevelConfig : ScriptableObject
  {
    [SerializeField] private Level _prefab;
    [SerializeField] private List<Vector3> _enemyWaypoints;
    [SerializeField] private WaveSettings[] _waves;
    [SerializeField] private int _startMoney;
    [SerializeField] private float _timeBetweenWaves;
    [SerializeField] private AudioClip _music;

    public Level Prefab => _prefab;
    public int StartMoney => _startMoney;
    public float TimeBetweenWaves => _timeBetweenWaves;
    public List<Vector3> EnemyWaypoints => _enemyWaypoints;
    public WaveSettings[] Waves => _waves;
    public AudioClip Music => _music;
  }
}