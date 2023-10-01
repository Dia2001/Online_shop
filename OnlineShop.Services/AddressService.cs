using OnlineShop.Core;
using OnlineShop.Core.Schemas;

namespace OnlineShop.Services.Base
{

  public interface IAddress : IBaseService<AddressSchema>
  {

  }

  public class AddressService : BaseService<AddressSchema, DataContext>, IAddress
  {

    private readonly DataContext context;
    public AddressService(DataContext _ctx) : base(_ctx)
    {
      context = _ctx;
    }
  }
}
