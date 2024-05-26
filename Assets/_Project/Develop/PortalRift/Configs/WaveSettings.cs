using System;

namespace PortalRift.Configs
{
  [Serializable]
  public class WaveSettings
  {
    public EnemyType[] Enemies;
    public int EnemiesCount;
    public float EnemySpawnRate;
  }
}