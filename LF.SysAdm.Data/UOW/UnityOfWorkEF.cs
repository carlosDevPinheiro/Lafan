using LF.SysAdm.Data.Context;
using LF.SysAdm.Domain.UOW;

namespace LF.SysAdm.Data.UOW
{
    public class UnityOfWorkEF : IUnityOfWork
    {
        private readonly DbContextEF _context;
        public UnityOfWorkEF(IDbConnectionContext context)
        {
            _context = context as DbContextEF;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Rollback()
        {
            //
        }
    }
}
