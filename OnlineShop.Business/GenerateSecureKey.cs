using System.Security.Cryptography;

namespace OnlineShop.Business
{
  public static class GenerateSecureKey
  {
    public static string GenerateRandomKey(int length)
    {
      using (var rng = RandomNumberGenerator.Create())
      {
        var bytes = new byte[length];
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
      }
    }
  }

}
