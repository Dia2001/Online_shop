
using OnlineShop.Core;
using OnlineShop.Core.Schemas;
using OnlineShop.Core.Schemas.Base;
using OnlineShop.Services.Base;
using OnlineShop.Utils;

namespace OnlineShop.Services
{
  public interface ICustomer : IBaseService<CustomerSchema>
  {
    CustomerSchema Create(CustomerSchema customer);
    List<CustomerSchema> GetByName(string name);
    List<CustomerSchema> Yanji();
    bool CheckPermissionAction(int customer, string endPoint);
    CustomerSchema Authentication(string name, string password);

  }

  public class CustomerService : BaseService<CustomerSchema, DataContext>, ICustomer
  {

    private readonly DataContext context;

    public CustomerService(DataContext _ctx) : base(_ctx)
    {
      this.context = _ctx;
    }

    public List<CustomerSchema> GetByName(string name)
    {
      List<CustomerSchema> customers = context.Customers.Where(u => !string.IsNullOrEmpty(name) && u.UserName.Equals(name)).ToList();
      return customers;
    }

    public CustomerSchema Authentication(string name, string password)
    {
      List<CustomerSchema> customers = GetByName(name);
      string hashedPassword = PwdUtil.ConvertToDecrypt(password);

      CustomerSchema customer = customers
         .Where(u => !string.IsNullOrEmpty(u.Password))
         .Where(u => PwdUtil.ConvertToDecrypt(u.Password) == hashedPassword).FirstOrDefault();

      return customer;
    }

    public CustomerSchema UpdateLoginTime(int customerId)
    {
      CustomerSchema customer = context.Customers.Find(customerId);
      customer.LastLogin = DateTime.UtcNow;
      return customer;
    }

    public CustomerSchema SetRefreshToken(string refreshToken, int customerId)
    {
      CustomerSchema customer = context.Customers.Find(customerId);
      customer.HashRefreshToken = refreshToken;
      return customer;
    }

    public List<CustomerSchema> Yanji()
    {
      List<CustomerSchema> Customers = (from c in context.Customers
                                        select new CustomerSchema()
                                        {
                                          Phonenumber = c.Phonenumber
                                        }
                                        ).ToList();
      return Customers;
    }

    public bool CheckPermissionAction(int customerId, string endPoint)
    {
      PermSchema perm = (
        from p in context.Perms
        join gp in context.GroupsPerms on p.Id equals gp.PermId
        join g in context.Groups on gp.GroupId equals g.Id
        join cg in context.CustomersGroups on g.Id equals cg.GroupId
        where cg.CustomerId == customerId && p.Action == endPoint
        select p
      ).FirstOrDefault();

      return perm != null;
    }
  }
}
