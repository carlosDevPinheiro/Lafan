using LF.SysAdm.Data.Context;
using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Querys.Supply;
using LF.SysAdm.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LF.SysAdm.Data.Repositorys.EF
{
    public class RepositorySupplyEF : CRUDEF<Supply>, IRepositorySupply
    {
        private readonly DbContextEF _context;
        public RepositorySupplyEF(IDbConnectionContext context) : base(context)
        {
            _context = (DbContextEF)context;
        }

        public SupplyQuery GetSupply(Guid Id)
        {
            var supply = GetEntity(Id);
            return new SupplyQuery
            {
                AddressId = supply.Rel_Address.ID,
                Phone = supply.Phone,
                Agent = supply.Agent,
                CNPJ = supply.CNPJ,
                CompanyName = supply.CompanyName,
                DateOfChange = supply.DateOfChange,
                DateRegister = supply.DateRegister,
                Email = supply.Email,
                ID = supply.ID

            };
        }

        public SupplyQuery GetSupplyCNPJ(string cnpj)
        {
            var query = (from supply in _context.Set<Supply>()
                         where supply.CNPJ == cnpj
                         select new SupplyQuery
                         {
                             ID = supply.ID,
                             Agent = supply.Agent,
                             CNPJ = supply.CNPJ,
                             CompanyName = supply.CompanyName,
                             DateOfChange = supply.DateOfChange,                             
                             DateRegister = supply.DateRegister,
                             Email = supply.Email,
                             Phone = supply.Phone

                         }).SingleOrDefault();

            return query;
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
            return (from supply in _context._dbSetSupply
                    join address in _context._dbsetaddress on supply.Rel_Address.ID equals address.ID
                    select new SupplyWithAddressQuery
                    {
                        ID = supply.ID,
                        Agent = supply.Agent,
                        AddressId = supply.Rel_Address.ID,
                        Email = supply.Email,
                        CompanyName = supply.CompanyName,
                        CNPJ = supply.CNPJ,
                        DateRegister = supply.DateRegister,
                        DateOfChange = supply.DateOfChange,
                        Street = address.Street,
                        Number = address.Number,
                        Complement = address.Complement,
                        CEP = address.CEP,
                        City = address.City,
                        District = address.District,
                        Phone = supply.Phone,
                        State = address.State
                    }).FirstOrDefault();
            
        }       
    }
}
