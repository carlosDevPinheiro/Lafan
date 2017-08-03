using LF.SysAdm.Shared.Validations;
using System;

namespace LF.SysAdm.Domain.Entity.Base
{
    public abstract class BaseEntity: Notifiable
    {
        public Guid ID { get; private set; }
        public BaseEntity()
        {
            ID = Guid.NewGuid();
        }

        public abstract void Register();

        
    }
}
