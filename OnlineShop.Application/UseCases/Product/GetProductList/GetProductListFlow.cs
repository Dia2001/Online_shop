using OnlineShop.Application.UseCase; 
using OnlineShop.Services.Base;
using OnlineShop.Utils;

namespace OnlineShop.Application.UseCases.Product.GetProductList
{
	public class GetProductListFlow
	{
		private readonly IUnitOfWork uow;
		public GetProductListFlow(IUnitOfWork _uow)
		{
			uow = _uow;
		}

		public Response List()
		{
			var products = uow.Products.FindAll();
			return new Response(Message.SUCCESS, products);
		}
	}
}
