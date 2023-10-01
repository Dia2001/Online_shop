using AutoMapper;
using OnlineShop.Core.Schemas;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Application.UseCases.User.Crud.Presenter
{
  public class CreateProductPresenter
  {
    [Required]
    public int CategoryID { get; set; }
    [Required]
    public int BrandID { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public float Price { get; set; }
    [Required]
    public int Discount { get; set; }
    [Required]
    public string Currentcy { get; set; }
    public string? DefaultImage { get; set; }
    [Required]
    public string OriginLinkDetail { get; set; }
    public string Url { get; set; }
    public int Stock { get; set; }
  }

  public class CreateProductMapping : Profile
  {
    public CreateProductMapping()
    {
      // Default mapping when property names are same
      CreateMap<CreateProductPresenter, ProductSchema>();

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
