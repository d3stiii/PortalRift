namespace PortalRift.Runtime.Data.Session
{
  public class SessionData
  {
    public readonly MoneyData MoneyData;
    public readonly WavesData WavesData;

    public SessionData()
    {
      MoneyData = new MoneyData();
      WavesData = new WavesData();
    }
  }
}