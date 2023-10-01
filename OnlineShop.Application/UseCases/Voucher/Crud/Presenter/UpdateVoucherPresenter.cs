using AutoMapper;
using OnlineShop.Core.Schemas;

namespace OnlineShop.Application.UseCase.Voucher.Crud.Presenter
{
  public class UpdateVoucherPresenter
  {
    public string? Code { get; set; }
    public string? Desc { get; set; }
    public decimal DiscountAmount { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool Status { get; set; }
    public int CurrentUsageCount { get; set; }
  }


  public class UpdateVoucherMapping : Profile
  {
    public UpdateVoucherMapping()
    {
      CreateMap<UpdateVoucherPresenter, VoucherSchema>();
    }
  }
}