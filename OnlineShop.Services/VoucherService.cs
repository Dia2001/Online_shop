using OnlineShop.Core;
using OnlineShop.Core.Schemas;


namespace OnlineShop.Services.Base
{

  public interface IVoucher : IBaseService<VoucherSchema>
  {
    List<VoucherSchema> GetByDiscountAmount(decimal discountAmount);
    List<VoucherSchema> GetVouchers();
    string CheckExpiryDate(string voucherCode);
    VoucherSchema Get(string voucherCode);
    VoucherSchema DeleteByCode(string voucherCode);
    dynamic ApplyVoucher(int productId, string voucherCode, string freeshipCode);
  }

  public class VoucherService : BaseService<VoucherSchema, DataContext>, IVoucher
  {

    private readonly DataContext context;
    public VoucherService(DataContext _ctx) : base(_ctx)
    {
      context = _ctx;
    }

    public List<VoucherSchema> GetByDiscountAmount(decimal discountAmount)
    {
      List<VoucherSchema> vouchers = context.Vouchers.Where(v => v.DiscountAmount.Equals(discountAmount)).ToList();
      return vouchers;
    }

    public List<VoucherSchema> GetVouchers()
    {
      List<VoucherSchema> vouchers = (
        from v in context.Vouchers
        select v
      ).ToList();

      return vouchers;
    }

    public dynamic ApplyVoucher(int productId, string voucherCode, string freeshipCode)
    {

      var productService = new ProductService(context);
      var product = productService.Get(productId);
      float priceAfterDiscount = 0;

      if (voucherCode != null)
      {

        var voucher = Get(voucherCode);

        if (voucher.DiscountAmount != 0)
        {
          priceAfterDiscount = product.Price - (float)voucher.DiscountAmount;
        }
        else if (voucher.DiscountPercent != 0)
        {
          priceAfterDiscount = product.Price * (float)(voucher.DiscountPercent / 100);
        }
      }

      if (priceAfterDiscount <= 0)
      {
        priceAfterDiscount = 0;
      }

      if (freeshipCode != null)
      {
        product.IsFreeship = true;
      }

      return new { priceAfterDiscount = priceAfterDiscount, isFreeShip = product.IsFreeship };
    }

    public VoucherSchema Get(string voucherCode)
    {
      VoucherSchema voucher = (
        from v in context.Vouchers
        where v.Code == voucherCode
        select v
      ).FirstOrDefault();

      return voucher!;
    }

    public string CheckExpiryDate(string voucherCode)
    {
      VoucherSchema voucher = Get(voucherCode);
      bool isExpired = DateTime.Now > voucher.EndDate;
      var result = isExpired ? "Expired" : "Not expired";
      return result;
    }

    public VoucherSchema DeleteByCode(string voucherCode)
    {
      var voucher = (
        from v in context.Vouchers
        where v.Code == voucherCode
        select v
      ).FirstOrDefault();

      if (voucher == null)
      {
        return voucher;
      }

      context.Set<VoucherSchema>().Remove(voucher);
      context.SaveChanges();
      return voucher;
    }
  }
}
