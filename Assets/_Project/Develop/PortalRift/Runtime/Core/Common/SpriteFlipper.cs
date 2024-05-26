using UnityEngine;

namespace PortalRift.Runtime.Core.Common
{
  public class SpriteFlipper : MonoBehaviour
  {
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Vector3 _previousPosition;

    private void Awake() => 
      _previousPosition = transform.position;

    private void Update()
    {
      var direction = transform.position - _previousPosition;
      _spriteRenderer.flipX = direction.x switch
      {
        < 0 => false,
        > 0 => true,
        _ => _spriteRenderer.flipX
      };

      _previousPosition = transform.position;
    }
  }
}