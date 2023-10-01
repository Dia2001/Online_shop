using AutoMapper;
using OnlineShop.Core.Schemas.Base;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Application.UseCases.User.Crud.Presenter
{
    public class CreateUserPresenter
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class CreateUserMapping : Profile
    {
        public CreateUserMapping()
        {
            // Default mapping when property names are same
            CreateMap<CreateUserPresenter, UserSchema>();

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
