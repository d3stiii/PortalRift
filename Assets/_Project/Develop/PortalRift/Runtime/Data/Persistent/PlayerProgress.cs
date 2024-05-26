using System;

namespace PortalRift.Runtime.Data.Persistent
{
  [Serializable]
  public class PlayerProgress
  {
    public int LastCompletedLevel;
    public bool TutorialCompleted;

    public void CompleteLevel(int index)
    {
      if (LastCompletedLevel > index)
        return;

      LastCompletedLevel = index;
    }
  }
}