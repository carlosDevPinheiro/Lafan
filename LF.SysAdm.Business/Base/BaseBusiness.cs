using LF.SysAdm.Domain.UOW;
using LF.SysAdm.Shared.Validations;

namespace LF.SysAdm.Business.Base
{
    public class BaseBusiness 
    {
        IUnityOfWork _uow;
        public BaseBusiness(IUnityOfWork uow)
        {
            _uow = uow;
        }

        public bool Commit(Notifiable entity)
        {
            if (entity.IsNotification())
            {
                _uow.Commit();
                return true;
            }
            else
            {
                _uow.Rollback();
                return false;
            }
        }

         
    }
}
