using LF.SysAdm.Domain.Querys.Address;
using System;

namespace LF.SysAdm.Domain.Querys.Supply
{
    public class SupplyQuery : BaseQuery
    {
        public Guid ID { get; set; }
        public string CompanyName { get;  set; }
        public string CNPJ { get;  set; }
        public string Phone { get;  set; }
        public string Agent { get;  set; }
        public string Email { get;  set; }
        public DateTime DateRegister { get; set; }
        public DateTime? DateOfChange { get;  set; }
        public Guid AddressId { get;  set; }
       
    }
}
