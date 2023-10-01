
using Microsoft.AspNetCore.Http.HttpResults;
using OnlineShop.Core.Schemas;

namespace OnlineShop.Application.UseCases.Customer.Crud.Presenter
{
  public class CrudCustomerPresenter
  {
    public static List<CustomerSchema> PresentList(List<CustomerSchema> items)
    {
      var result = new List<CustomerSchema>();
      foreach (var item in items)
      {
        var customer = PresentItem(item);
        result.Add(customer);
      }
      return result;
    }

    public static CustomerSchema PresentItem(CustomerSchema item)
    {
      CustomerSchema customer = new CustomerSchema();
      customer.Id = item.Id;
      customer.UserName = item.UserName;
      customer.Phonenumber = item.Phonenumber;
      customer.Address = item.Address;
      return customer;
    }
  }
}
