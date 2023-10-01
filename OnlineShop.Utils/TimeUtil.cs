namespace OnlineShop.Utils
{
  public class TimeUtil
  {
    public enum TIME_UNIX
    {
      MINUTE = 60,
      HOUR = MINUTE * 60,
      SEVEN_HOUR = HOUR * 7,
      DAY = HOUR * 24,
      WEEK = DAY * 7,
    }

    public struct TIMEZONE_ID
    {
      public const string GMT7 = "SE Asia Standard Time";
      public const string GMT0 = "Greenwich Standard Time";
    }
  }
}
