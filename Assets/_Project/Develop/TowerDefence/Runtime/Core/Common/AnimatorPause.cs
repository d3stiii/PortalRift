using TowerDefence.Core.Pause;
using UnityEngine;

namespace TowerDefence.Core.Common
{
  public class AnimatorPause : MonoBehaviour, IPauseHandler
  {
    [SerializeField] private Animator _animator;

    public void SetPause(bool isPaused) => 
      _animator.speed = isPaused ? 0 : 1;
  }
}