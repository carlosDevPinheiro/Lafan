using System;

namespace LF.SysAdm.Domain.Querys.Customer
{
    public class CustomerWithUserQuery: CustomerQuery
    {
        public string Email { get; set; }
        public string Name { get; set; }       
    }
}
