
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineShop.Core.Schemas.Base;

namespace OnlineShop.Core.Schemas
{
  public class ShipmentSchema : BaseSchema
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string ShippingMethod { get; set; }
    public float ShippingCost { get; set; }
    public bool Discountable { get; set; }
    public int OrderId { get; set; }
    public ICollection<OrderSchema> Order { get; set; }
  }
}
