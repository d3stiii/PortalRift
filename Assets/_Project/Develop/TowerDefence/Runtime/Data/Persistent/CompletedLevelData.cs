using System;

namespace TowerDefence.Data.Persistent
{
  [Serializable]
  public class CompletedLevelData
  {
    public int LastCompletedLevel;

    public void CompleteLevel(int index)
    {
      if (LastCompletedLevel > index)
        return;

      LastCompletedLevel = index;
    }
  }
}