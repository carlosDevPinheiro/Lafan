using LF.SysAdm.Domain.Entity.Base;
using LF.SysAdm.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LF.SysAdm.Domain.Entity
{
    public class Order : BaseEntity
    {
        protected Order() { ListItens = new List<OrderItem>(); }

        public Order(Customer cust, Employee emp, EPayment pay)
        {
            OrderDate = DateTime.Now;
            Status = EOrderStatus.Create;
            Total = ListItens.Sum(x => x.Price);
            Rel_Customer = cust;
            CustomerId = cust.ID;
            Rel_Employee = emp;
            EmployeeId = emp.ID;
            ListItens = new List<OrderItem>();            
        }

        
        public DateTime OrderDate { get; private set; }
        public DateTime ChangeDate { get; private set; }
        public EOrderStatus Status { get; private set; }
        public decimal Total { get; private set; }
        public ICollection<OrderItem> ListItens { get; private set; }        
        public string Comments { get; private set; }
        public EPayment PaymentMethod { get; private set; }
        public decimal Discount { get; private set; }

        public Guid CustomerId { get; private set; }       
        public Guid EmployeeId { get; private set; }

        public Employee Rel_Employee { get; private set; }
        public Customer Rel_Customer { get; private set; }

        public override void Register()
        {
           // throw new NotImplementedException();
        }

        public void AlterOrder(EOrderStatus status)
        {
            Status = status;
            ChangeDate = DateTime.Now;
        }
        public void AlterOrder(EPayment pay)
        {
            PaymentMethod = pay;
            ChangeDate = DateTime.Now;
        }
        public void AlterOrder(decimal discount)
        {
            Discount -= discount;
            ChangeDate = DateTime.Now;
        }
    }
}
