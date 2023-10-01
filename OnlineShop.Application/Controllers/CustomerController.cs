using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.UseCases.Auth;
using OnlineShop.Application.UseCases.Customer.Crud;
using OnlineShop.Core.Schemas;
using OnlineShop.Services.Base;
using OnlineShop.Utils;

namespace OnlineShop.Application.Controllers
{
  public class CustomerController : Controller
  {
    CrudCustomerFlow crudCustomerFlow;
    public CustomerController(IUnitOfWork uow)
    {
      crudCustomerFlow = new CrudCustomerFlow(uow);
    }

    public IActionResult Register()
    {
      return View();
    }

    [HttpPost(Name = "Create_")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(CustomerSchema customer)
    {
      if (customer.Phonenumber == 0)
      {
        ModelState.AddModelError("Phonenumber", "The Phonenumber field is required.");
      }

      if (!ModelState.IsValid)
      {
        return View("Index", customer);
      }

      await crudCustomerFlow.Create(customer);
      return RedirectToAction("Index", "Home");
    }

    public IActionResult Login()
    {
      return View();
    }

    public const string SessionKeyUsername = "_Username";

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(CustomerSchema customer)
    {
      var response = crudCustomerFlow.Login(customer.UserName, customer.Password);

      if (!ModelState.IsValid || response.Status == "error")
      {
        return View("Login", customer);
      }

      LoginPresenter tokens = (LoginPresenter)response.Result;
      CookieOptions cookieOptions = JwtUtil.GetConfigOption();

      Response.Cookies.Append("AccessToken", tokens.AccessToken, cookieOptions);
      Response.Cookies.Append("RefreshToken", tokens.RefreshToken, cookieOptions);

      var username = HttpContext.Session.GetString(SessionKeyUsername);

      if (string.IsNullOrEmpty(username))
      {
        HttpContext.Session.SetString(SessionKeyUsername, customer.UserName);
      }

      return RedirectToAction("Index", "Home");
    }


    // [Authorize]
    [HttpGet("Logout", Name = "LogoutCustomer_")]
    public async Task<IActionResult> Logout()
    {
      foreach (var cookie in Request.Cookies.Keys)
      {
        Response.Cookies.Delete(cookie);
      }
      return RedirectToAction("Index", "Home");
    }
  }
}
