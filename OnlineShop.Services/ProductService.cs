using OnlineShop.Core;
using OnlineShop.Core.Schemas;
using OnlineShop.Services.Base;

namespace OnlineShop.Services
{
	public interface IProduct : IBaseService<ProductSchema>
	{
		ProductSchema Get(int productId);
	}

	public class ProductService : BaseService<ProductSchema, DataContext>, IProduct
	{

		private readonly DataContext context;

		public ProductService(DataContext _ctx) : base(_ctx)
		{
			this.context = _ctx;
		}

		public ProductSchema Get(int productId)
		{
			ProductSchema product = (
			  from p in context.Products
			  where p.Id == productId
			  select p
			).FirstOrDefault();

			return product!;
		}

	}
}
