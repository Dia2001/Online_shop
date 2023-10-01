
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.UseCase;
using OnlineShop.Application.UseCases.Auth;
using OnlineShop.Application.UseCases.Customer.Crud.Presenter;
using OnlineShop.Core.Schemas;
using OnlineShop.Services.Base;
using OnlineShop.Utils;

namespace OnlineShop.Application.UseCases.Customer.Crud
{
  [ApiController]
  [Route("api/customers")]
  public class CustomerCtrl : ControllerBase
  {
    private readonly IUnitOfWork uow;
    private readonly IMapper _mapper;
    CrudCustomerFlow workFlow;
    public CustomerCtrl(IUnitOfWork _uow, IMapper mapper)
    {
      uow = _uow;
      _mapper = mapper;
      workFlow = new CrudCustomerFlow(_uow);
    }

    [HttpPost("register", Name = "CreateCustomer_")]
    // public async Task<IActionResult> Create([FromBody] CustomerSchema model)
    public async Task<IActionResult> Create([FromBody] CreateCustomerPresenter model)
    {
      CustomerSchema customer = _mapper.Map<CustomerSchema>(model);
      Response response = await workFlow.Create(customer);
      if (response.Status == Message.ERROR)
      {
        return BadRequest();
      }
      return Ok(response);
    }

    [HttpGet("list", Name = "ListCustomer_1")]
    public async Task<IActionResult> List(string sortName = "Id", string sortType = "ASC", int cursor = 0, int pageSize = 10)
    {
      Response response = workFlow.List();
      List<CustomerSchema> items = (List<CustomerSchema>)response.Result;
      CtrlUtil.ApplySort<CustomerSchema, string>(ref items, sortName, sortType);
      ResponsePresenter res = CtrlUtil.ApplyPaging<CustomerSchema, string>(cursor, pageSize, items);

      if (response.Status == Message.ERROR)
      {
        return BadRequest();
      }

      res.Items = CrudCustomerPresenter.PresentList((List<CustomerSchema>)res.Items);

      return Ok(res);
    }

    [HttpPost("login", Name = "LoginCustomer_")]
    public async Task<IActionResult> Login([FromBody] GetCustomerPresenter model)
    {
      Response response = workFlow.Login(model.Username, PwdUtil.ConvertToEncrypt(model.Password));
      if (response.Status == Message.ERROR)
      {
        return Unauthorized();
      }

      LoginPresenter token = (LoginPresenter)response.Result;
      CookieOptions cookieOptions = JwtUtil.GetConfigOption();
      Response.Cookies.Append(JwtUtil.ACCESS_TOKEN, token.AccessToken, cookieOptions);
      Response.Cookies.Append(JwtUtil.REFRESH_TOKEN, token.RefreshToken);

      return Ok(response);
    }

    [HttpGet(Name = "GetCustomer_1")]
    public async Task<IActionResult> Get(int id)
    {
      Response response = workFlow.Get(id);
      if (response.Status == Message.ERROR)
      {
        return BadRequest();
      }

      return Ok(response);
    }

    [HttpPut(Name = "UpdateCustomer_3")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerPresenter model)
    {
      CustomerSchema user = _mapper.Map<CustomerSchema>(model);
      user.Id = id;

      Response response = await workFlow.Update(user);

      if (response.Status == Message.ERROR)
      {
        return BadRequest();
      }

      return Ok(response);
    }

    [HttpDelete("delete", Name = "DeleteCustomer_1")]
    public async Task<IActionResult> Delete(int id)
    {
      Response response = workFlow.Delete(id);
      if (response.Status == Message.ERROR)
      {
        return BadRequest();
      }
      return Ok(response);
    }

    [HttpDelete(Name = "DeleteGroup_1")]
    public async Task<IActionResult> Delete(int[] ids)
    {
      Response response = workFlow.Deletes(ids);
      if (response.Status == Message.ERROR)
      {
        return BadRequest();
      }

      return Ok(response);
    }
  }
}
