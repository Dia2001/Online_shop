using OnlineShop.Utils;
using Microsoft.AspNetCore.Mvc;
using AutoMapper; 
using OnlineShop.Services.Base;
using OnlineShop.Application.UseCase;
using OnlineShop.Application.UseCases.User.Crud.Presenter;
using OnlineShop.Core.Schemas.Base; 

namespace OnlineShop.Application.UseCases.User.Crud
{
    [ApiController]
    [Route("api/users")]
    public class CrudUserController : ControllerBase
    {
        private readonly IMapper _mapper;
        CrudUserFlow workFlow;
        public CrudUserController(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            workFlow = new CrudUserFlow(uow);
        }


        [HttpGet(Name = "ListUser_1")]
        public async Task<IActionResult> List(string sortName = "Id", string sortType = "ASC", int cursor = 0, int pageSize = 10)
        {
            Response response = workFlow.List();
            List<UserSchema> items = (List<UserSchema>)response.Result;
            CtrlUtil.ApplySort<UserSchema, string>(ref items, sortName, sortType);
            ResponsePresenter res = CtrlUtil.ApplyPaging<UserSchema, string>(cursor, pageSize, items);
            if (response.Status == Message.ERROR)
            {
                return BadRequest();
            }
            res.Items = CrudUserPresenter.PresentList((List<UserSchema>)res.Items);
            return Ok(res);
        }

        [HttpPost(Name = "CreateUser_1")]
        public async Task<IActionResult> Create([FromBody] CreateUserPresenter model)
        {
            UserSchema user = _mapper.Map<UserSchema>(model);
            Response response = await workFlow.Create(user);
            if (response.Status == Message.ERROR)
            {
                return BadRequest();
            }
            return Ok(response);
        }
 
        [HttpPut(Name = "UpdateUser_1")]
        public async Task<IActionResult> Update([FromBody] UpdateUserPresenter model)
        {
            UserSchema user = _mapper.Map<UserSchema>(model);
            Response response = await workFlow.Update(user);
            if (response.Status == Message.ERROR)
            {
                return BadRequest();
            }
            return Ok(response);
        }

        [HttpDelete(Name = "DeleteUser_1")]
        public async Task<IActionResult> Delete(int id)
        {
            Response response = workFlow.Delete(id);
            if (response.Status == Message.ERROR)
            {
                return BadRequest();
            }
            return Ok(response);
        }
    }
}
