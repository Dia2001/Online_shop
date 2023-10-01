using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Application.Controllers
{
  public class CheckoutController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
