
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineShop.Core.Schemas.Base;

namespace OnlineShop.Core.Schemas
{
  public class VoucherSchema : BaseSchema
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Code { get; set; }
    public string? Desc { get; set; }
    public double DiscountAmount { get; set; }
    public double DiscountPercent { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Status { get; set; }
    public int MaxUsageCount { get; set; }
    public int CurrentUsageCount { get; set; }
    public ICollection<OrderVoucher> OrderVouchers { get; set; }
  }
}
