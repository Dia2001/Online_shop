

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineShop.Core.Schemas.Base;

namespace OnlineShop.Core.Schemas
{
  public class CustomerSchema : BaseSchema
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string UserName { get; set; }
    public int Phonenumber { get; set; }
    public string Password { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public ICollection<OrderSchema>? Orders { get; set; }
    public DateTime? LastLogin { get; set; }
    public string? HashRefreshToken { get; set; }
  }
}
