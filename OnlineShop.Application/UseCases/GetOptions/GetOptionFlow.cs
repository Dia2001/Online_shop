using OnlineShop.Application.UseCase;
using OnlineShop.Services.Base;
using OnlineShop.Utils;

namespace OnlineShop.Application.UseCases.GetOptions
{
    public class GetOptionFlow
    {
        private readonly IUnitOfWork uow;
        public GetOptionFlow(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public Response GetOptions()
        {
            var users = uow.Users.FindAll();
            return new Response(Message.SUCCESS, users);
        }


    }
}
