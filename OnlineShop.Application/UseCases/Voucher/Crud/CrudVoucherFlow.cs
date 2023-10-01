using OnlineShop.Utils;
using OnlineShop.Services.Base;
using OnlineShop.Application.UseCase;
using OnlineShop.Core.Schemas;
using AutoMapper.Configuration.Conventions;

namespace OnlineShop.Application.UseCases.Voucher.Crud
{
  public class CrudVoucherFlow
  {
    private readonly IUnitOfWork uow;
    public CrudVoucherFlow(IUnitOfWork _uow)
    {
      uow = _uow;
    }

    public async Task<Response> Create(VoucherSchema voucher)
    {
      var result = uow.Vouchers.Create(voucher);
      return new Response(Message.SUCCESS, result);
    }

    public async Task<Response> Update(VoucherSchema voucher)
    {
      var result = uow.Vouchers.Update(voucher);
      return new Response(Message.SUCCESS, result);
    }


    public Response List()
    {
      var vouchers = uow.Vouchers.GetVouchers();
      return new Response(Message.SUCCESS, vouchers);
    }

    public Response Delete(int id)
    {
      var result = uow.Vouchers.Delete(id);
      return new Response(Message.SUCCESS, result);
    }

    public Response CheckExpiryDate(string voucherCode)
    {
      var result = uow.Vouchers.CheckExpiryDate(voucherCode);
      return new Response(Message.SUCCESS, result);
    }

    public Response DeleteByCode(string voucherCode) {
      var result = uow.Vouchers.DeleteByCode(voucherCode);
      return new Response(Message.SUCCESS, result);
    }

    public Response ApplyVoucher(int productId, string voucherCode, string freeshipCode = null) {
      var result = uow.Vouchers.ApplyVoucher(productId, voucherCode, freeshipCode);
      return new Response(Message.SUCCESS, result);
    }

    // public Response Deletes(int[] ids)
    // {
    //   var result = uow.Vouchers.Deletes(ids);
    //   return new Response(Message.SUCCESS, result);
    // }
  }
}
