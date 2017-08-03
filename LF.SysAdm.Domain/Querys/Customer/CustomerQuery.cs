using LF.SysAdm.Domain.Querys.Address;
using LF.SysAdm.Domain.Querys.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.SysAdm.Domain.Querys.Customer
{
    public class CustomerQuery: BaseQuery
    {
        public Guid CustomerId { get; set; }
        public string Document { get; set; }
        public DateTime DateBirthday { get; set; }
        public string Phone { get; set; }
        public bool Gender { get; set; }
        public DateTime DateRegister { get; set; }
        public DateTime? DateOfChange { get; set; }
        public Guid UserId { get; set; } 
    }
}
