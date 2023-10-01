using AutoMapper;
using OnlineShop.Core.Schemas;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Application.UseCases.Voucher.Crud.Presenter
{
  public class CreateVoucherPresenter
  {
    [Required]
    public string Code { get; set; }
    [Required]
    public double DiscountAmount { get; set; }
    [Required]
    public double DiscountPercent { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public bool Status { get; set; }
  }

  public class CreateVoucherMapping : Profile
  {
    public CreateVoucherMapping()
    {
      CreateMap<CreateVoucherPresenter, VoucherSchema>();
    }
  }
}
