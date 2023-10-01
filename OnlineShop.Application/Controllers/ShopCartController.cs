using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Application.Controllers
{
  public class ShopCartController : Controller
  {
    [Authorize]
    public IActionResult Index()
    {
      return View();
    }
  }
}
