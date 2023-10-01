using System.Text;

namespace OnlineShop.Business.SecureVoucherCode
{
  public static class SecureVoucherCode
  {
    public static string Key = GenerateSecureKey.GenerateRandomKey(40);

    public static string ConvertToEncrypt(string voucherCode)
    {
      if (string.IsNullOrEmpty(voucherCode)) return "";

      voucherCode += Key;
      var voucherCodeBytes = Encoding.UTF8.GetBytes(voucherCode);
      return Convert.ToBase64String(voucherCodeBytes);
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
