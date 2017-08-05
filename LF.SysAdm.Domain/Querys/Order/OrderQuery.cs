using LF.SysAdm.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.SysAdm.Domain.Querys.Order
{
    public class OrderQuery: BaseQuery
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ChangeDate { get; set; }
        public EOrderStatus Status { get; set; }
        public decimal Total { get; set; }
        public string Comments { get; set; }
        public EPayment PaymentMethod { get; set; }
        public string Discount { get; set; }
    }
}
