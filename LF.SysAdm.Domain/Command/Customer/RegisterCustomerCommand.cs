using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.SysAdm.Domain.Command.Customer
{
    public class RegisterCustomerCommand
    {
        public string Document { get; set; }
        public DateTime DateBirthday { get; set; }
        public string Phone { get; set; }
        public bool Gender { get; set; }

        public string Email { get; set; }
        public Guid UserId { get; set; }

        public string Street { get; set; }
        public int Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CEP { get; set; }
    }
}
