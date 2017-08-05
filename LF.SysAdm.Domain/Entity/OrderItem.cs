using LF.SysAdm.Domain.Entity.Base;
using LF.SysAdm.Shared.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.SysAdm.Domain.Entity
{
    public class OrderItem :BaseEntity
    {
        protected OrderItem() { }

        public OrderItem(Product prod, int quant)
        {
            ProdId = prod.ID;
            ItemName = prod.Name;
            Price = prod.Price;
            Quantity = quant;
        }

        public OrderItem(ServiceProvide service,int quant)
        {
            ProdId = service.ID;
            ItemName = service.ServiceName;
            Price = service.Price;
            Quantity = quant;
        }

        public string ItemName { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public Guid ProdId { get; set; }

        public Guid OrderId { get; set; }
        public Order Rel_Order { get; set; }

        public override void Register()
        {
           // throw new NotImplementedException();
        }
    }
}
