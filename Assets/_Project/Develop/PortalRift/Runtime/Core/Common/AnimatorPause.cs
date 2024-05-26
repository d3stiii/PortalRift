using PortalRift.Runtime.Core.Pause;
using UnityEngine;

namespace PortalRift.Runtime.Core.Common
{
  public class AnimatorPause : MonoBehaviour, IPauseHandler
  {
    [SerializeField] private Animator _animator;

    public void SetPause(bool isPaused) => 
      _animator.speed = isPaused ? 0 : 1;
  }
}