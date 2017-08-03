using LF.SysAdm.Data.Context;
using LF.SysAdm.Domain.UOW;

namespace LF.SysAdm.Data.UOW
{
    public class UnityOfWorkDapper : IUnityOfWork
    {
        DbContextDapper _context;
        bool _disposed;
        public UnityOfWorkDapper(IDbConnectionContext context)
        {
            _context = (DbContextDapper)context;
        }
        
        
        public void Commit()
        {
            try
            {
                DbContextDapper.Transaction.Commit();
            }
            finally
            {
                DbContextDapper.Transaction = DbContextDapper.Connection.BeginTransaction();
            } 
        }

        public void Rollback()
        {
            try
            {
                DbContextDapper.Transaction.Rollback();
            }
            finally
            {
                DbContextDapper.Transaction = DbContextDapper.Connection.BeginTransaction();
            }
        }

        public void ResetConnection()
        {
            if(DbContextDapper.Transaction != null)
            {
                DbContextDapper.Transaction.Dispose();
                DbContextDapper.Transaction = null;
            }
            if(DbContextDapper.Connection != null)
            {
                DbContextDapper.Connection.Dispose();
                DbContextDapper.Connection = null;
            }
            
        }

        public void Dispose()
        {
             dispose(true);
        }

        public void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    ResetConnection();

                    _disposed = true;
                }
            }
        }

    }
}


