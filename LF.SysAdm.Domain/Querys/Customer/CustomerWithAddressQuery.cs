using LF.SysAdm.Domain.Querys.Address;
using System;
using System.Collections.Generic;

namespace LF.SysAdm.Domain.Querys.Customer
{
    public class CustomerWithAddressQuery: CustomerQuery
    {  
        public IEnumerable<AddressQuery> ListAddress { get; set; }
    }
}
