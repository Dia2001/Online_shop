using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OnlineShop.Services.Base;
using OnlineShop.Application.UseCase;

namespace OnlineShop.Application.UseCases.SyncAllPerm
{
    [ApiController]
    [Route("api/seed")]
    public class SeedController : ControllerBase
    {
        SeedFlow flow;
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
        public SeedController(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider, IUnitOfWork uow)
        {
            flow = new SeedFlow(uow);
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        [HttpGet(Name = "Seed_")]
        public async Task<IActionResult> Seed()
        {
            var routes = _actionDescriptorCollectionProvider.ActionDescriptors.Items
               .Where(ad => ad.AttributeRouteInfo != null)
               .Select(x => new RouterPresenter
               {
                   Action = null != x && null != x.RouteValues && null != x.RouteValues["action"] ? x.RouteValues["action"] : "n/a",
                   Module = null != x && null != x.RouteValues && null != x.RouteValues["controller"] ? x.RouteValues["controller"] : "n/a",
                   Name = x.AttributeRouteInfo.Name ?? "n/a",
                   Template = x.AttributeRouteInfo.Template ?? "n/a",
                   Method = x.ActionConstraints?.OfType<HttpMethodActionConstraint>().FirstOrDefault()?.HttpMethods.First()
               }).ToList();
            Response response = await flow.Seed(routes);

            return Ok(response);
        }
    }
}