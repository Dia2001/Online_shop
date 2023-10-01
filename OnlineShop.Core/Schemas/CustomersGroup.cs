using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Core.Schemas.Base
{
  public class CustomersGroups
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerGroupId { get; set; }

    public int GroupId { get; set; }
    public int CustomerId { get; set; }
    public CustomerSchema Customer { get; set; }
    public GroupSchema Group { get; set; }
  }
}
