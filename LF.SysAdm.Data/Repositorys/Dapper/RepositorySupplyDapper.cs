using Dapper;
using LF.SysAdm.Data.Context;
using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Querys.Supply;
using LF.SysAdm.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LF.SysAdm.Data.Repositorys.Dapper
{
    public class RepositorySupplyDapper : CRUDDapper<Supply>, IRepositorySupply
    {
        private readonly DbContextDapper _context;

        public RepositorySupplyDapper(IDbConnectionContext context) : base(context)
        {
            _context = (DbContextDapper)context;
        }

        public SupplyQuery GetSupply(Guid Id)
        {
            var Supply = GetEntity(Id);
            return new SupplyQuery
            {
                AddressId = Supply.Rel_Address.ID,
                Agent = Supply.Agent,
                CNPJ = Supply.Agent,
                CompanyName = Supply.CompanyName,
                DateOfChange = Supply.DateOfChange,
                DateRegister = Supply.DateRegister,
                Email = Supply.Email,
                ID = Supply.ID,
                Phone = Supply.Phone
            };
        }

        public SupplyQuery GetSupplyCNPJ(string cnpj)
        {
            string SqlCmd = $"SELECT * FROM [dbo].Supply WHERE [CNPJ] = '{cnpj}'";
           
            return DbContextDapper.Connection.QueryFirstOrDefault<SupplyQuery>(SqlCmd, transaction: DbContextDapper.Transaction);
        }

        public IEnumerable<SupplyQuery> GetSupplyes()
        {
            var list = GetAllEntity();
            return list.Select(x => new SupplyQuery
            {
                AddressId = x.Rel_Address.ID,
                Phone = x.Phone,
                Agent = x.Agent,
                CNPJ = x.CNPJ,
                CompanyName = x.CompanyName,
                DateOfChange = x.DateOfChange,
                DateRegister = x.DateRegister,
                Email = x.Email,
                ID = x.ID

            }).ToList();
        }

        public SupplyWithAddressQuery GetSupplyWithAddress(Guid Id)
        {
            string SqlCmd = $"SELECT * FROM [LF_SupplyWithAddress] WHERE [ID] = '{Id}'";

          
            var query = DbContextDapper.Connection.QueryFirst<SupplyWithAddressQuery>(SqlCmd, transaction: DbContextDapper.Transaction);

            return query;
        }
    }
}
