using System;

namespace PortalRift.Runtime.Data.Session
{
  public class MoneyData
  {
    public event Action Changed;
    
    public int Value { get; private set; }

    public void AddMoney(int amount)
    {
      if (amount < 0)
        throw new ArgumentOutOfRangeException(nameof(amount),
          "Credits amount should be more than zero!");

      Value += amount;
      Changed?.Invoke();
    }

    public void SpendMoney(int amount)
    {
      if (amount < 0)
        throw new ArgumentOutOfRangeException(nameof(amount),
          "Credits amount should be more than zero!");

      Value -= amount;
      Changed?.Invoke();
    }
  }
}