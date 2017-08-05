using Dapper;
using LF.SysAdm.Data.Context;
using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Enum;
using LF.SysAdm.Domain.Querys.Order;
using LF.SysAdm.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Data;

namespace LF.SysAdm.Data.Repositorys.Dapper
{
    public class RepositoryOrderDapper : CRUDDapper<Order>, IRepositoryOrder
    {
        private readonly DbContextDapper _context;
        private string SqlCmd = string.Empty;
        public RepositoryOrderDapper(IDbConnectionContext context) : base(context)
        {
            _context = (DbContextDapper)context;
        }

        public OrderInvoiceQuery GetOrder(Guid orderId)
        {
            var parames = new DynamicParameters();
            parames.Add("@ID", orderId, DbType.Guid);

           return DbContextDapper.Transaction
                .Connection.QueryFirstOrDefault<OrderInvoiceQuery>(
                "LF_OrderInvoice", param: parames, transaction: DbContextDapper.Transaction,
                commandType: CommandType.StoredProcedure);           
        }

        public IEnumerable<OrderQuery> GetOrders(EOrderStatus status)
        {
            var parames = new DynamicParameters();
            parames.Add("@STATUS", status, DbType.Int32);
            SqlCmd = "SELECT * FROM [LF_GetOrders] WHERE [Status] = @STATUS";

            return DbContextDapper.Transaction
                .Connection.Query<OrderQuery>(SqlCmd, param: parames, transaction: DbContextDapper.Transaction);
        }

        public IEnumerable<OrderQuery> GetOrders(DateTime start, DateTime end)
        {
            
            var parames = new DynamicParameters();
            parames.Add("@START", start, DbType.DateTime);
            parames.Add("@END", end, DbType.DateTime);           

            return DbContextDapper.Transaction
                .Connection.Query<OrderQuery>(
                "LF_GetOrdersDate", param: parames,
                transaction: DbContextDapper.Transaction,
                commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<OrderQuery> GetOrders(Guid CustomerId)
        {
            var parames = new DynamicParameters();
            parames.Add("@CustomerId", CustomerId, DbType.Guid);
            SqlCmd = "SELECT * FROM [LF_GetOrders] WHERE [CustomerId] = @CustomerId";

            return DbContextDapper.Transaction
                .Connection.Query<OrderQuery>(SqlCmd, param: parames, transaction: DbContextDapper.Transaction);
        }

        public IEnumerable<OrderQuery> GetOrders(EPayment pay)
        {
            var parames = new DynamicParameters();
            parames.Add("@Payment", pay, DbType.Int32);
            SqlCmd = "SELECT * FROM [LF_GetOrders] WHERE [PaymentMethod] = @Payment";

            return DbContextDapper.Transaction
                .Connection.Query<OrderQuery>(SqlCmd, param: parames, transaction: DbContextDapper.Transaction);
        }

        public IEnumerable<OrderQuery> GetOrders(int skip = 0, int take = 10)
        {
            var parames = new DynamicParameters();
            parames.Add("@SKIP", skip, DbType.Int32);
            parames.Add("@TAKE", take, DbType.Int32);

            return DbContextDapper.Transaction
                .Connection.Query<OrderQuery>(
                "LF_GetOrders", 
                param: parames, 
                transaction: DbContextDapper.Transaction, 
                commandType: CommandType.StoredProcedure);
        }
    }
}
