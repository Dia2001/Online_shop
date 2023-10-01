using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Application.UseCase.Voucher
{
  public class VoucherPresenter
  {
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
}