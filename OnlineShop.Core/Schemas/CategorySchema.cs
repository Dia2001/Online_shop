

using OnlineShop.Core.Schemas.Base;

namespace OnlineShop.Core.Schemas
{
	public class CategorySchema : BaseSchema
	{
		public List<ProductSchema> Products { get; set; }
	}
}
