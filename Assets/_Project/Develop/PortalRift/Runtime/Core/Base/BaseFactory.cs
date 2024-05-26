using PortalRift.Runtime.Common.Extensions;
using PortalRift.Runtime.Services.Configs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace PortalRift.Runtime.Core.Base
{
  public class BaseFactory
  {
    private readonly ConfigProvider _configProvider;
    private readonly IObjectResolver _objectResolver;

    public BaseFactory(IObjectResolver objectResolver, ConfigProvider configProvider)
    {
      _objectResolver = objectResolver;
      _configProvider = configProvider;
    }

    public GameObject CreateBase(Vector3 at) =>
      _objectResolver
        .Instantiate(_configProvider.BaseConfig.Prefab.transform, at, Quaternion.identity).gameObject
        .With(x => x.GetComponent<BaseHealth>()
          .Construct(_configProvider.BaseConfig));
  }
}