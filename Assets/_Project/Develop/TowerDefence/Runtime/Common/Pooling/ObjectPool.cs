using System;
using System.Collections.Generic;
using System.Linq;
using Object = UnityEngine.Object;

namespace TowerDefence.Common.Pooling
{
  public class ObjectPool<T> : IDisposable where T : Object
  {
    public event Action OnRelease;
    
    private readonly Func<T> _create;
    private readonly Action<T> _get;
    private readonly List<T> _list;
    private readonly Action<T> _release;


    public ObjectPool(Func<T> createFunc, Action<T> get = null, Action<T> release = null)
    {
      _list = new List<T>();
      _create = createFunc ?? throw new ArgumentNullException(nameof(createFunc));
      _get = get;
      _release = release;
    }

    public int CountAll { get; private set; }


    public void Dispose() =>
      Clear();

    public T Get()
    {
      T obj;
      if (_list.Count == 0)
      {
        obj = _create();
        ++CountAll;
      }
      else
      {
        var index = _list.Count - 1;
        obj = _list[index];
        _list.RemoveAt(index);
      }

      _get?.Invoke(obj);
      return obj;
    }

    public T Get(Predicate<T> predicate)
    {
      T obj;
      if (_list.Count == 0)
      {
        obj = _create();
        ++CountAll;
      }
      else
      {
        var index = _list.FindIndex(predicate);
        var isFound = index != -1;
        if (isFound)
        {
          obj = _list[index];
          _list.RemoveAt(index);
        }
        else
        {
          obj = _create();
          ++CountAll;
        }
      }

      _get?.Invoke(obj);
      return obj;
    }

    public void Release(T element)
    {
      if (_list.Count > 0 && _list.Any(t => element == t))
        throw new InvalidOperationException(
          "Trying to release an object that has already been released to the pool.");

      OnRelease?.Invoke();
      _release?.Invoke(element);
      _list.Add(element);
    }

    public void Clear()
    {
      foreach (var obj in _list)
        Object.Destroy(obj);

      _list.Clear();
      CountAll = 0;
    }
  }
}