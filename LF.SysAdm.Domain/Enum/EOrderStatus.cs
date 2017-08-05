using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.SysAdm.Domain.Enum
{
    public enum EOrderStatus
    {
        Create = 1,
        InAnalysis = 2,
        Paid = 3,
        Shipped = 4, // despachado
        Canceled = 5,
        Processing = 6 // em processamento  
    }
}
