using AutoMapper;
using OnlineShop.Core.Schemas;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Application.UseCases.Customer.Crud.Presenter
{
  public class CreateCustomerPresenter
  {
    [Required]
    public string Username { get; set; }

    [Required]
    public int Phonenumber { get; set; }
    [Required]
    public string Password { get; set; }
  }

  public class CreateCustomerMapping : Profile
  {
    public CreateCustomerMapping()
    {
      // Default mapping when property names are same
      CreateMap<CreateCustomerPresenter, CustomerSchema>();

      // Mapping when property names are different
      //CreateMap<User, UserViewModel>()
      //    .ForMember(dest =>
      //    dest.FName,
      //    opt => opt.MapFrom(src => src.FirstName))
      //    .ForMember(dest =>
      //    dest.LName,
      //    opt => opt.MapFrom(src => src.LastName));
    }
  }
}
