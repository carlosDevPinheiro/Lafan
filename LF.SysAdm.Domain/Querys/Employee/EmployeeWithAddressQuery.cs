using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.SysAdm.Domain.Querys.Employee
{
    public class EmployeeWithAddressQuery
    {
        public Guid EmployeeId { get; set; }
        public string Name { get; set; }
        public string Function { get; set; }
        public string Department { get; set; }
        public string Document { get; set; }
        public DateTime DateBirthday { get; set; }
        public DateTime DateRegister { get; set; }
        public DateTime? DateOfChange { get; set; }
        public string RE { get; set; }

        public Guid AddressId { get; set; }        
        public string Street { get; set; }
        public int Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CEP { get; set; }
    }
}
