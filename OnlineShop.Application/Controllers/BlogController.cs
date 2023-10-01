using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Application.Controllers
{
  public class BlogController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
