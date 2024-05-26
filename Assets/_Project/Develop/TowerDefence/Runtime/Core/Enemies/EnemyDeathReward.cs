using TowerDefence.Configs;
using TowerDefence.Services.Data;
using UnityEngine;
using VContainer;

namespace TowerDefence.Core.Enemies
{
  [RequireComponent(typeof(EnemyDeath))]
  public class EnemyDeathReward : MonoBehaviour
  {
    private EnemyDeath _enemyDeath;
    private SessionDataProvider _sessionDataProvider;
    private int _value;

    [Inject]
    public void Inject(SessionDataProvider sessionDataProvider) => 
      _sessionDataProvider = sessionDataProvider;

    public void Construct(EnemyConfig config) => 
      _value = config.MoneyForKill;

    private void Awake()
    {
      _enemyDeath = GetComponent<EnemyDeath>();
      _enemyDeath.Died += OnDeath;
    }

    private void OnDeath() => 
      _sessionDataProvider.SessionData.MoneyData.AddMoney(_value);
  }
}