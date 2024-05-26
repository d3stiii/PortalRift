using UnityEngine;

namespace PortalRift.Runtime.Common.UI
{
  public class BaseWindow : MonoBehaviour
  {
    private void Awake() =>
      OnAwake();

    private void Start()
    {
      Initialize();
      SubscribeUpdates();
    }

    private void OnEnable() =>
      OnOpening();

    private void OnDestroy() =>
      Cleanup();


    protected virtual void OnAwake()
    {
    }

    protected virtual void Initialize()
    {
    }

    protected virtual void OnOpening()
    {
    }

    protected virtual void SubscribeUpdates()
    {
    }

    protected virtual void UnsubscribeUpdates()
    {
    }

    protected virtual void Cleanup() =>
      UnsubscribeUpdates();
  }
}