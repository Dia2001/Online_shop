using System.Text.RegularExpressions;

namespace OnlineShop.Business.GetDiscountPercentVoucherCode
{
  public class GetDiscountPercentVoucherCode
  {
    public static double getDiscountPercentVoucherCode(string specialVoucherCode)
    {
      double numberDiscountPercent = double.Parse(Regex.Match(specialVoucherCode, @"\d+").Value);

      return numberDiscountPercent;
    }
  }
}
