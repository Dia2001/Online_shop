using OnlineShop.Core;
using OnlineShop.Core.Schemas;
using OnlineShop.Services.Base;

namespace OnlineShop.Services
{
  public interface IShipment : IBaseService<ShipmentSchema>
  {

  }



  public class ShipmentService : BaseService<ShipmentSchema, DataContext>, IShipment
  {

    private readonly DataContext context;

    public ShipmentService(DataContext _ctx) : base(_ctx)
    {
      this.context = _ctx;
    }

  }
}
