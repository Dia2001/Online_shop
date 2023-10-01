using OnlineShop.Utils;
using OnlineShop.Services.Base;
using OnlineShop.Application.UseCase;
using OnlineShop.Core.Schemas;
using OnlineShop.Application.UseCases.Auth;

namespace OnlineShop.Application.UseCases.Customer.Crud
{
  public class CrudCustomerFlow
  {
    private readonly IUnitOfWork uow;
    public CrudCustomerFlow(IUnitOfWork _uow)
    {
      uow = _uow;
    }

    public async Task<Response> Create(CustomerSchema user)
    {
      user.Password = PwdUtil.ConvertToEncrypt(user.Password);
      var result = uow.Customers.Create(user);
      return new Response(Message.SUCCESS, result);
    }

    public async Task<Response> Update(CustomerSchema user)
    {
      var result = uow.Customers.Update(user);
      return new Response(Message.SUCCESS, result);

    }

    public Response Login(string username, string password)
    {
      CustomerSchema user = uow.Customers.Authentication(username, password);

      if (user == null)
      {
        return new Response(Message.ERROR, new { });
      }

      string accessToken = JwtUtil.GenerateAccessToken(user.Id);
      string refreshToken = JwtUtil.GenerateRefreshToken();
      uow.Users.SetRefreshToken(refreshToken, user.Id);
      uow.Users.UpdateLoginTime(user.Id);
      return new Response(Message.SUCCESS, new LoginPresenter { AccessToken = accessToken, RefreshToken = refreshToken });
    }

    public Response List()
    {
      var customers = uow.Customers.Yanji();
      return new Response(Message.SUCCESS, customers);
    }

    public Response Get(int id)
    {
      var customer = uow.Customers.FindOne(id);
      return new Response(Message.SUCCESS, customer);
    }

    public Response Delete(int id)
    {
      var result = uow.Customers.Delete(id);
      return new Response(Message.SUCCESS, result);
    }

    public Response Deletes(int[] ids)
    {
      var result = uow.Customers.Deletes(ids);
      return new Response(Message.SUCCESS, result);
    }
  }
}
