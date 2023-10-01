using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.UseCases.Customer.Crud;
using OnlineShop.Services.Base;

namespace OnlineShop.Application.Controllers
{
    public class ContactController : Controller
    {
        CrudCustomerFlow crudCustomerFlow;
        public ContactController(IUnitOfWork uow)
        {
            crudCustomerFlow = new CrudCustomerFlow(uow);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
