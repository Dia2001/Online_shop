
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.UseCase;
using OnlineShop.Application.UseCase.Voucher.Crud.Presenter;
using OnlineShop.Application.UseCases.Voucher.Crud.Presenter;
using OnlineShop.Business.GenerateVoucherCode;
using OnlineShop.Core.Schemas;
using OnlineShop.Services.Base;
using OnlineShop.Utils;

namespace OnlineShop.Application.UseCases.Voucher.Crud
{
  [ApiController]
  [Route("api/vouchers")]
  public class CrudVoucherCtrl : ControllerBase
  {
    private readonly IUnitOfWork uow;
    private readonly IMapper _mapper;
    CrudVoucherFlow workFlow;
    public CrudVoucherCtrl(IUnitOfWork _uow, IMapper mapper)
    {
      uow = _uow;
      _mapper = mapper;
      workFlow = new CrudVoucherFlow(_uow);

    }

    [HttpPost("create", Name = "CreateVoucher_1")]
    public async Task<IActionResult> Create([FromBody] CreateVoucherPresenter model)
    {
      if (model.Code == "string")
      {
        model.Code = GenerateVoucherCode.randomCodeVoucher();
      }
      VoucherSchema voucher = _mapper.Map<VoucherSchema>(model);
      Response response = await workFlow.Create(voucher);
      if (response.Status == Message.ERROR)
      {
        return BadRequest();
      }
      return Ok(response);
    }

    [HttpDelete("delete/{voucherCode}", Name = "DeleteVoucher_1")]
    public async Task<IActionResult> Delete(string voucherCode)
    {
      Response response = workFlow.DeleteByCode(voucherCode);
      if (response.Status == Message.ERROR)
      {
        return BadRequest();
      }
      return Ok(response);
    }

    [HttpGet("expiryDate/{voucherCode}", Name = "CheckExpiryDate_")]
    public async Task<IActionResult> CheckExpiryDate(string voucherCode)
    {
      Response response = workFlow.CheckExpiryDate(voucherCode);
      if (response.Status == Message.ERROR)
      {
        return BadRequest();
      }

      return Ok(response);
    }

    [HttpPut("update", Name = "UpdateVoucher_1")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateVoucherPresenter model)
    {
      VoucherSchema voucher = _mapper.Map<VoucherSchema>(model);
      voucher.Id = id;

      Response response = await workFlow.Update(voucher);

      if (response.Status == Message.ERROR)
      {
        return BadRequest();
      }

      return Ok(response);
    }

    [HttpGet("list", Name = "ListVoucher_")]
    public async Task<IActionResult> List(string sortName = "Id", string sortType = "ASC", int cursor = 0, int pageSize = 10)
    {
      Response response = workFlow.List();
      List<VoucherSchema> items = (List<VoucherSchema>)response.Result;

      if (sortName == "Id")
      {
        if (sortType == "ASC")
        {
          items = items.OrderBy(item => item.Id).ToList();
        }
        else if (sortType == "DESC")
        {
          items = items.OrderByDescending(item => item.Id).ToList();
        }
      }

      ResponsePresenter res = CtrlUtil.ApplyPaging<VoucherSchema, string>(cursor, pageSize, items);

      if (response.Status == Message.ERROR)
      {
        return BadRequest();
      }

      res.Items = CrudVoucherPresenter.PresentList((List<VoucherSchema>)res.Items);

      return Ok(res);
    }

    [HttpPost("applyVoucher", Name = "ApplyVoucher_")]
    public async Task<IActionResult> ApplyVoucher(int productId, string voucherCode, string? freeshipCode)
    {
      Response response = workFlow.ApplyVoucher(productId, voucherCode, freeshipCode);

      if (response.Status == Message.ERROR)
      {
        return BadRequest();
      }

      return Ok(response);
    }
  }
}
