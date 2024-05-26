using System.Collections;
using UnityEngine;

namespace PortalRift.Runtime.Common
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
    void StopCoroutine(IEnumerator coroutine);
    void StopCoroutine(Coroutine coroutine);
  }

  public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
  {
  }
}