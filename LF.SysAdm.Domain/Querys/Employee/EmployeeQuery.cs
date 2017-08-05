using LF.SysAdm.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.SysAdm.Domain.Querys.Employee
{
    public class EmployeeQuery : BaseQuery
    {
        public Guid EmployeeId { get; set; }
        public string Name { get; set; }        
        public string Document { get; set; }
        public DateTime DateBirthday { get; set; }
        public DateTime DateRegister { get; set; }
        public DateTime? DateOfChange { get; set; }
        public string RE { get; set; }             
        public string Function { get; set; }
        public string Department { get; set; }
    }
}
