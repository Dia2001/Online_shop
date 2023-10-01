
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Utils;
using OnlineShop.Services.Base;
using Microsoft.AspNetCore.Authorization;
using OnlineShop.Application.UseCase;

namespace OnlineShop.Application.UseCases.Auth
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        AuthFlow workFlow;
        public AuthController(IUnitOfWork uow)
        {
            workFlow = new AuthFlow(uow);
        }

        [HttpPost("login", Name = "Login_")]
        public IActionResult Login([FromBody] AuthPresenter model)
        { 
            try
            {
                Response response = workFlow.Login(model.Username, model.Password);
                if (response.Status == Message.ERROR)
                {
                    return Unauthorized();
                }
                LoginPresenter token = (LoginPresenter)response.Result;
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("refresh-token", Name = "RefreshToken_")]
        public IActionResult RefreshToken([FromBody] LoginPresenter tokenParam)
        {

            if (string.IsNullOrWhiteSpace(tokenParam.RefreshToken))
            {
                return Unauthorized();
            }

            Response response = workFlow.RefreshToken(tokenParam.AccessToken, tokenParam.RefreshToken);
            if (response.Status == Message.ERROR)
            {
                return Unauthorized();
            }
            LoginPresenter token = (LoginPresenter)response.Result;
            return Ok(token);
        }

        [Authorize]
        [HttpGet("logout", Name = "Logout_")]
        public IActionResult Logout()
        {
            string token = Request.Headers[JwtUtil.ACCESS_TOKEN];
            if (token == null)
            {
                return Unauthorized();
            }
            Response.Cookies.Append(JwtUtil.ACCESS_TOKEN, null);
            return Ok();
        }
    }
}