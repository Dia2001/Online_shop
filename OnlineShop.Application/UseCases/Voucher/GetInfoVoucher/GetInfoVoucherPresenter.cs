
using OnlineShop.Core.Schemas;

namespace OnlineShop.Application.UseCases.Voucher.Crud.Presenter
{
  public class GetInfoVoucherPresenter
  {
    public static List<VoucherSchema> PresentList(List<VoucherSchema> items)
    {
      var result = new List<VoucherSchema>();
      foreach (var item in items)
      {
        var voucher = PresentItem(item);
        result.Add(voucher);
      }
      return result;
    }

    public static VoucherSchema PresentItem(VoucherSchema item)
    {

      VoucherSchema voucher = new VoucherSchema();
      voucher.Id = item.Id;
      voucher.DiscountAmount = Math.Round(item.DiscountAmount, 2);
      voucher.DiscountPercent = Math.Round(item.DiscountPercent, 2);
      voucher.CurrentUsageCount = item.CurrentUsageCount;
      voucher.StartDate = item.StartDate;
      voucher.EndDate = item.EndDate;
      voucher.Status = item.Status;
      voucher.Code = item.Code;
      voucher.Desc = item.Desc;
      voucher.MaxUsageCount = item.MaxUsageCount;
      voucher.CurrentUsageCount = item.CurrentUsageCount;

      return voucher;
    }

  }
}
