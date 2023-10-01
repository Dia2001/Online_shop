using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Application.UseCases.Auth
{
    public class AuthPresenter
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
