using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.SysAdm.Domain.Command.Supply
{
    public class EditeSupplyCommand
    {
        public Guid SupplyId { get; set; }
        public string CompanyName { get; set; }
        public string CNPJ { get; set; }
        public string Phone { get; set; }
        public string Agent { get; set; }
        public string Email { get; set; }
    }
}
