using LF.SysAdm.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LF.SysAdm.Data.Context;
using LF.SysAdm.Domain.Repositorys;

namespace LF.SysAdm.Data.Repositorys.EF
{
    public class RepositoryAddressEF : CRUDEF<Address>, IRepositoryAddress
    {
        private readonly DbContextEF _context;
        public RepositoryAddressEF(IDbConnectionContext context) : base(context)
        {
            _context = (DbContextEF)context;
        }
    }
}
