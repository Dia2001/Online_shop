using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OnlineShop.Core.Schemas.Base;

namespace OnlineShop.Core.Schemas
{
    public class OrderVoucher : BaseSchema
    {
        public int OrderId { get; set; }
        public OrderSchema Order { get; set; }

        public int VoucherId { get; set; }
        public VoucherSchema Voucher { get; set; }
    }
}