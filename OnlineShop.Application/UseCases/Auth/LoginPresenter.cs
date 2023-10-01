using OnlineShop.Core.Schemas.Base;

namespace OnlineShop.Application.UseCases.Auth
{
    public class LoginPresenter
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public UserSchema User { get; set; }
    }
}
