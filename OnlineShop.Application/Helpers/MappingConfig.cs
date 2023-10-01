using OnlineShop.Application.UseCases.User.Crud.Presenter;
using OnlineShop.Application.UseCases.Voucher.Crud.Presenter;

namespace OnlineShop.Application.Helpers
{
    public class MappingConfig
    {
        public static void AutoMapperConfig(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CreateUserMapping));
            services.AddAutoMapper(typeof(CreateVoucherMapping));
        }
    }
}
