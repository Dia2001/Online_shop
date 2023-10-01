using OnlineShop.Core;
using OnlineShop.Core.Schemas.Base;

namespace OnlineShop.Services.Base
{

  public interface ICustomerGroup : IBaseService<CustomersGroups>
  {
    List<CustomersGroups> Creates(List<CustomersGroups> customersGroups);
  }

  public class CustomerGroupService : BaseService<CustomersGroups, DataContext>, ICustomerGroup
  {

    private readonly DataContext context;
    public CustomerGroupService(DataContext _ctx) : base(_ctx)
    {
      context = _ctx;
    }

    public List<CustomersGroups> Creates(List<CustomersGroups> customersGroups)
    {
      context.CustomersGroups.AddRange(customersGroups);
      context.SaveChanges();
      return customersGroups;
    }
  }
}
