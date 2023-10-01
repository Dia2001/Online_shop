using OnlineShop.Utils;
using OnlineShop.Services.Base;
using OnlineShop.Application.UseCase;

namespace OnlineShop.Application.UseCases.Voucher.GetCurrentVoucher
{

  public class GetInfoVoucherFlow
  {
    private readonly IUnitOfWork uow;
    public GetInfoVoucherFlow(IUnitOfWork _uow)
    {
      uow = _uow;
    }

    public Response Get(string voucherCode)
    {
      var result = uow.Vouchers.Get(voucherCode);
      return new Response(Message.SUCCESS, result);
    }
  }
}
