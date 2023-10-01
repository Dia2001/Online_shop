using OnlineShop.Utils;
using OnlineShop.Services.Base;
using OnlineShop.Application.UseCase;
using OnlineShop.Core.Schemas.Base;

namespace OnlineShop.Application.UseCases.User.Crud
{

    public class CrudUserFlow
    {
        private readonly IUnitOfWork uow;
        public CrudUserFlow(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public Response GetCurrentUser(string token)
        {
            string userCredentialString = JwtUtil.GetIdByToken(token);
            int id = 0;
            if (!int.TryParse(userCredentialString, out id))
            {
                return new Response(Message.ERROR, null);
            }
            UserSchema user = uow.Users.FindOne(id);
            return new Response(Message.SUCCESS, user);
        }

        public Response List()
        {
            var users = uow.Users.FindAll();
            return new Response(Message.SUCCESS, users);
        }

        public async Task<Response> Create(UserSchema user)
        {
            var result = uow.Users.Create(user);
            return new Response(Message.SUCCESS, result);
        }

        public async Task<Response> Update(UserSchema user)
        {
            var result = uow.Users.Update(user);
            return new Response(Message.SUCCESS, result);
        }

        public Response Delete(int id)
        {
            var result = uow.Users.Delete(id);
            return new Response(Message.SUCCESS, result);
        }

    }
}
