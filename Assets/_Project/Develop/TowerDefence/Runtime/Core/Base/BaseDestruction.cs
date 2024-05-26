using TowerDefence.Core.Results;
using UnityEngine;
using VContainer;

namespace TowerDefence.Core.Base
{
  [RequireComponent(typeof(BaseHealth))]
  public class BaseDestruction : MonoBehaviour
  {
    private BaseHealth _baseHealth;
    private GameResult _gameResult;

    [Inject]
    public void Construct(GameResult gameResult) => 
      _gameResult = gameResult;

    private void Awake()
    {
      _baseHealth = GetComponent<BaseHealth>();
      _baseHealth.HealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged()
    {
      if (_baseHealth.CurrentHp <= 0)
        Destruct();
    }

    private void Destruct()
    {
      _gameResult.Lose();
      Destroy(gameObject);
    }
  }
}