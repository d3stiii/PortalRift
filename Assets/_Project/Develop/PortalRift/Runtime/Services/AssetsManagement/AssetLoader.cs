using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PortalRift.Runtime.Services.AssetsManagement
{
  public class AssetLoader
  {
    public TAsset Load<TAsset>(string path) where TAsset : Object
    {
      var asset = Resources.Load<TAsset>(path);

      if (asset == null) throw new ArgumentException($"Incorrect asset path: {path}", nameof(path));

      return asset;
    }

    public TAsset[] LoadAll<TAsset>(string path) where TAsset : Object
    {
      var assets = Resources.LoadAll<TAsset>(path);

      if (assets == null || assets.Length == 0)
        throw new ArgumentException($"Incorrect assets path: {path}", nameof(path));

      return assets;
    }
  }
}