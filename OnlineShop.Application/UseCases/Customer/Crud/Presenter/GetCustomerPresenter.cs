using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Application.UseCases.Customer
{
  public class GetCustomerPresenter
  {
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
  }
}
