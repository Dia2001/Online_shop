using OnlineShop.Application.UseCase;
using OnlineShop.Core.Schemas;
using OnlineShop.Services.Base;
using OnlineShop.Utils;

namespace OnlineShop.Application.UseCases.Product.Crud
{
	public class CrudProductFlow
	{
		private readonly IUnitOfWork uow;
		public CrudProductFlow(IUnitOfWork _uow)
		{
			uow = _uow;
		}

		public async Task<Response> Create(ProductSchema product)
		{
			var result = uow.Products.Create(product);
			return new Response(Message.SUCCESS, result);
		}
	}
}
