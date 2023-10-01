using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Application.UseCases.Customer
{
  public class CustomerPresenter
  {
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public int Phonenumber { get; set; }
  }
}
