

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineShop.Core.Schemas.Base;

namespace OnlineShop.Core.Schemas
{
  public class OrderSchema : BaseSchema
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int CustomerID { get; set; }
    public int Quantity { get; set; }
    public float Price { get; set; }
    public ICollection<OrderVoucher> OrderVouchers { get; set; }
    public int ProductId { get; set; }
    public ICollection<ProductOrderSchema> ProductOrders { get; set; }
  }
}