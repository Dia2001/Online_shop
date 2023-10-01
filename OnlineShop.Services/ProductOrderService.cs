using OnlineShop.Core;
using OnlineShop.Core.Schemas;
using OnlineShop.Services.Base;

namespace OnlineShop.Services
{
  public interface IProductOrder : IBaseService<ProductOrderSchema>
  {

  }

  public class ProductOrderService : BaseService<ProductOrderSchema, DataContext>, IProductOrder
  {

    private readonly DataContext context;

    public ProductOrderService(DataContext _ctx) : base(_ctx)
    {
      this.context = _ctx;
    }

  }
}
