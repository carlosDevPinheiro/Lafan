using System;

namespace LF.SysAdm.Domain.UOW
{
    public interface IUnityOfWork: IDisposable
    {
        void Commit();
        void Rollback();

    }
}
