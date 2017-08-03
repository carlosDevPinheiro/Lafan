using Dapper;
using LF.SysAdm.Data.Context;
using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Querys.Address;
using LF.SysAdm.Domain.Querys.Customer;
using LF.SysAdm.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LF.SysAdm.Data.Repositorys.Dapper
{
    public class RepositoryCustomerDapper : CRUDDapper<Customer>, IRepositoryCustomer
    {
        private readonly DbContextDapper _context;
        private string SqlCmd = string.Empty;
        public RepositoryCustomerDapper(IDbConnectionContext context) : base(context)
        {
            _context = (DbContextDapper)context;           
        }

        public CustomerQuery GetCustomer(Guid Id)
        {           
            SqlCmd = $"SELECT CT.[ID] AS [CustomerId] ,CT.[Document],CT.[DateBirthday] ,CT.[Phone] ,CT.[Gender] ," +
                $" CT.[DateRegister] , CT.[DateOfChange], CT.[UserId]" +
                $" FROM[dbo].[Customer] AS CT" +
                $" WHERE CT.[ID] = '{Id}'";
           
           return  DbContextDapper.Transaction.Connection
                .QueryFirstOrDefault<CustomerQuery>(SqlCmd, transaction: DbContextDapper.Transaction);
        }

        public CustomerQuery GetCustomerCPF(string cpf)
        {
            SqlCmd = $"SELECT CT.[ID] AS [CustomerId] ,CT.[Document],CT.[DateBirthday] ,CT.[Phone] ,CT.[Gender] ," +
                $" CT.[DateRegister] , CT.[DateOfChange], CT.[UserId]" +
                $" FROM [dbo].[Customer] AS CT" +
                $" WHERE CT.[Document] = '{cpf}'";

           
            return DbContextDapper.Transaction.Connection
                .QueryFirstOrDefault<CustomerQuery>(SqlCmd, transaction: DbContextDapper.Transaction);
        }

        public CustomerWithAddressQuery GetCustomerWithAddress(Guid Id)
        {
            SqlCmd = $"SELECT C.[ID] AS [CustomerId] , C.[Document] , C.[DateBirthday] , C.[Phone] , C.[Gender] ," +
                $" C.[DateRegister] , C.[DateOfChange] , C.[UserId]" +
                $" FROM [dbo].[Customer] AS C WHERE C.[ID] = '{Id}';" +
                $" SELECT AD.[ID] AS [AddressId] ,AD.[Street] ,AD.[Number] ,AD.[Complement] ,AD.[District] ,AD.[City] ," +
                $" AD.[State] ,AD.[CEP]  FROM [dbo].[Address] AS AD" +
                $" INNER JOIN [dbo].[Customer] AS CT ON AD.[CustomerId] = CT.[ID] WHERE AD.[CustomerId] = '{Id}'";
           
            var query = DbContextDapper.Transaction.Connection.QueryMultiple(SqlCmd, transaction: DbContextDapper.Transaction);

            CustomerQuery customer = query.Read<CustomerQuery>().SingleOrDefault();
            IEnumerable<AddressQuery> ListAdress = query.Read<AddressQuery>().ToList();

            return new CustomerWithAddressQuery
            {
                CustomerId = customer.CustomerId,
                DateBirthday = customer.DateBirthday,
                DateOfChange = customer.DateOfChange,
                DateRegister = customer.DateRegister,
                Document = customer.Document,
                Gender = customer.Gender,
                Phone = customer.Phone,
                UserId = customer.UserId,
                ListAddress = ListAdress
            };
        }

        public CustomerWithUserQuery GetCutomerWithUser(Guid Id)
        {
            SqlCmd = $"SELECT CT.[ID] AS [CustomerId] ,CT.[Document],CT.[DateBirthday] ,CT.[Phone] ,CT.[Gender] , CT.[DateRegister] ," +
                $" CT.[DateOfChange],CT.[UserId] ,US.[Name] ,US.[Email]" +
                $" FROM [dbo].[Customer] AS CT INNER JOIN [dbo].[Users] AS US ON CT.[UserId] = US.[ID]" +
                $" WHERE CT.[ID] = '{Id}'";

            return DbContextDapper.Transaction.Connection.QueryFirst<CustomerWithUserQuery>(SqlCmd, transaction: DbContextDapper.Transaction);
        }
    }
}
