using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Querys;
using LF.SysAdm.Domain.Querys.Supply;
using System;
using System.Collections.Generic;

namespace LF.SysAdm.Domain.Repositorys
{
    public interface IRepositorySupply : ICRUD<Supply>
    {
        SupplyQuery GetSupply(Guid Id);
        SupplyQuery GetSupplyCNPJ(string cnpj);
        IEnumerable<SupplyQuery> GetSupplyes();
        SupplyWithAddressQuery GetSupplyWithAddress(Guid Id);
    }
}
