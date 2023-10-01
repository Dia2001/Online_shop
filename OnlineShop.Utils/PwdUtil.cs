using System.Text;

namespace OnlineShop.Utils
{
  public static class PwdUtil
  {
    public static string Key = "asdfasf@@@asdfadfsa";

    public static string ConvertToEncrypt(string password)
    {
      if (string.IsNullOrEmpty(password)) return "";

      password += Key;
      var passwordBytes = Encoding.UTF8.GetBytes(password);
      return Convert.ToBase64String(passwordBytes);
    }

    public static string ConvertToDecrypt(string base64EncodeData)
    {
      if (string.IsNullOrEmpty(base64EncodeData)) return "";

      var base64EncodeBytes = Convert.FromBase64String(base64EncodeData);
      var result = Encoding.UTF8.GetString(base64EncodeBytes);
      result = result.Substring(0, result.Length - Key.Length);
      return result;
    }
  }
}
