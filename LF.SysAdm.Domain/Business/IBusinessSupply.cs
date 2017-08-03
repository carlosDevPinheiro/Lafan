using LF.SysAdm.Domain.Command.Supply;
using LF.SysAdm.Domain.Querys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.SysAdm.Domain.Business
{
    public interface IBusinessSupply
    {
        ObjectRequest NewSupply(RegisterSupplyCommand cmd);
        ObjectRequest EditeSupply(EditeSupplyCommand cmd);
        ObjectRequest GetSupply(Guid Id);
        ObjectRequest GetSupply(string cnpj);
        ObjectRequest GetAllSupplys();
    }
}
