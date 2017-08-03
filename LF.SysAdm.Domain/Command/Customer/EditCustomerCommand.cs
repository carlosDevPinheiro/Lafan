using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.SysAdm.Domain.Command.Customer
{
    public class EditCustomerCommand
    {
        public Guid CustomerId { get; set; }
        public string Document { get; set; }
        public DateTime DateBirthday { get; set; }
        public string Phone { get; set; }
        public bool Gender { get; set; }
    }
}
