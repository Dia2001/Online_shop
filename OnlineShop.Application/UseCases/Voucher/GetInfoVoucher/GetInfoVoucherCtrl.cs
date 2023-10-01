
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.UseCase;
using OnlineShop.Services.Base;
using OnlineShop.Utils;

namespace OnlineShop.Application.UseCases.Voucher.GetCurrentVoucher
{
  [ApiController]
  [Route("api/vouchers")]
  public class GetInfoVoucherCtrl : ControllerBase
  {
    private readonly IUnitOfWork uow;
    private readonly IMapper _mapper;
    GetInfoVoucherFlow workFlow;
    public GetInfoVoucherCtrl(IUnitOfWork _uow, IMapper mapper)
    {
      uow = _uow;
      _mapper = mapper;
      workFlow = new GetInfoVoucherFlow(_uow);
    }

    [HttpGet("get-voucher/{voucherCode}", Name = "GetVoucherByCode_")]
    public async Task<IActionResult> Get(string voucherCode)
    {
      Response response = workFlow.Get(voucherCode);
      if (response.Status == Message.ERROR)
      {
        return BadRequest();
      }

      return Ok(response);
    }
  }
}
