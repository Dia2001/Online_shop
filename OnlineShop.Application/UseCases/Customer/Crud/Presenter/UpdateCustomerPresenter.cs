using AutoMapper;
using OnlineShop.Core.Schemas;

namespace OnlineShop.Application.UseCases.Customer.Crud.Presenter
{
  public class UpdateCustomerPresenter
  {
    public string Username { get; set; }
    public string Phonenumber { get; set; }
    public string Address { get; set; }
    public string Password {get;set;}

  }

  public class UpdateCustomerMapping : Profile
  {
    public UpdateCustomerMapping()
    {
      CreateMap<UpdateCustomerPresenter, CustomerSchema>();

    }
  }
}