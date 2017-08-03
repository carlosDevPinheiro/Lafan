using LF.SysAdm.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LF.SysAdm.Data.Context;
using LF.SysAdm.Domain.Repositorys;

namespace LF.SysAdm.Data.Repositorys.Dapper
{
    public class RepositoryAddressDapper : CRUDDapper<Address>, IRepositoryAddress
    {
        private readonly DbContextDapper _context;
        public RepositoryAddressDapper(IDbConnectionContext context) : base(context)
        {
            _context = (DbContextDapper)context;
        }
    }
}
