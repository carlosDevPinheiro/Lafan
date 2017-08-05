using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Enum;
using LF.SysAdm.Domain.Querys.Order;
using System;
using System.Collections.Generic;

namespace LF.SysAdm.Domain.Repositorys
{
    public interface IRepositoryOrder: ICRUD<Order>
    {
        IEnumerable<OrderQuery> GetOrders(EOrderStatus status);
        IEnumerable<OrderQuery> GetOrders(DateTime start, DateTime end);
        IEnumerable<OrderQuery> GetOrders(Guid CustomerId);         
        IEnumerable<OrderQuery> GetOrders(EPayment pay);
        IEnumerable<OrderQuery> GetOrders(int skip=0, int take=10);
        OrderInvoiceQuery GetOrder(Guid orderId);
       
    }
}
