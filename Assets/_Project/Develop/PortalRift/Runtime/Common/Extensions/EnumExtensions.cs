using System;
using Random = UnityEngine.Random;

namespace PortalRift.Runtime.Common.Extensions
{
  public static class EnumExtensions<T> where T : Enum
  {
    public static T GetRandomValue()
    {
      var values = Enum.GetValues(typeof(T));
      return (T)values.GetValue(Random.Range(0, values.Length));
    }
  }
}