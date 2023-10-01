

namespace OnlineShop.Business.GenerateVoucherCode
{
  public class GenerateVoucherCode
  {
    public static string randomCodeVoucher()
    {
      const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
      var code = new Random();
      char[] arr = new char[chars.Length];

      for (int i = 0; i < arr.Length; ++i)
      {
        arr[i] = chars[code.Next(chars.Length)];
      }

      return new string(arr).Substring(0, 6);
    }
  }
}
