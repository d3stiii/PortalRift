using UnityEngine;

namespace PortalRift.Runtime.Core.Enemies
{
  public class EnemyAnimator : MonoBehaviour
  {
    private static readonly int Die = Animator.StringToHash("Die");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int Direction = Animator.StringToHash("Direction");

    [SerializeField] private Animator _animator;

    public void StartMoving(Vector3 direction)
    {
      var roundedDirection = new Vector2(Mathf.RoundToInt(direction.x), Mathf.RoundToInt(direction.y));
      _animator.SetBool(IsMoving, true);

      if (roundedDirection.x != 0)
      {
        _animator.SetInteger(Direction, 0);
      }
      else
      {
        switch (roundedDirection.y)
        {
          case > 0:
            _animator.SetInteger(Direction, 1);
            break;
          case < 0:
            _animator.SetInteger(Direction, -1);
            break;
        }
      }
    }

    public void StopMoving() => 
      _animator.SetBool(IsMoving, false);

    public void PlayDeath() => 
      _animator.SetTrigger(Die);
  }
}