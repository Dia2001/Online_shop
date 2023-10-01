
using OnlineShop.Application.UseCase;
using OnlineShop.Services.Base;
using OnlineShop.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace OnlineShop.Application.UseCases.GetOptions
{

    [Authorize]
    [ApiController]
    [Route("api")]
    public class GetOptionController : ControllerBase
    {
        GetOptionFlow workFlow;
        public GetOptionController(IUnitOfWork uow)
        {
            workFlow = new GetOptionFlow(uow);
        }

        [HttpGet("options", Name = "OPTIONS_")]
        public IActionResult GetOption()
        {
            Response response = workFlow.GetOptions();
            if (response.Status == Message.ERROR)
            {
                return BadRequest();
            }
            return Ok(response.Result);
        }
    }
}
