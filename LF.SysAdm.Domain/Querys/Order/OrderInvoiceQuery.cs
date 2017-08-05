using LF.SysAdm.Domain.Entity;
using System;
using System.Collections.Generic;

namespace LF.SysAdm.Domain.Querys.Order
{
    public class OrderInvoiceQuery: BaseQuery
    {
        public Guid OrderId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid CustomerId { get; set; }
        public string Document { get; set; }       
        public string Phone { get; set; }

        public DateTime DateOrder { get; private set; }
        public DateTime DateOrderChanged { get; private set; }
        public string Status { get; private set; }
        public decimal Total { get; private set; }
        public ICollection<OrderItem> ListItens { get; private set; }
        public string Comments { get; private set; }
        public string PaymentMethod { get; private set; }
        public decimal Discount { get; private set; }

        public Guid AddressId { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CEP { get; set; }

    }
}
