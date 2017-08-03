using LF.SysAdm.Domain.Querys.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.SysAdm.Domain.Querys.Supply
{
    public class SupplyWithAddressQuery: SupplyQuery 
    {
        public string Street { get; set; }
        public int Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CEP { get; set; }
    }
}
