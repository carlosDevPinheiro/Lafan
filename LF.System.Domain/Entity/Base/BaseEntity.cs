using LF.System.Shared.Validation;
using System;

namespace LF.System.Domain.Entity.Base
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
