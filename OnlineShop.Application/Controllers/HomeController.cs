using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.UseCases.Product.GetProductList;
using OnlineShop.Services.Base;

namespace OnlineShop.Application.Controllers
{
    public class HomeController : Controller
    {
        GetProductListFlow getProductListFlow;
        public HomeController(IUnitOfWork uow)
        {
            getProductListFlow = new GetProductListFlow(uow); 
        }
        public IActionResult Index()
        {
            //var products = getProductListFlow.List();

            return View();
        }
    }
}
