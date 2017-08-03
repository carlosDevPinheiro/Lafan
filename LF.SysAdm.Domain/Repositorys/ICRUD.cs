using LF.SysAdm.Domain.Entity.Base;
using System;
using System.Collections.Generic;

namespace LF.SysAdm.Domain.Repositorys
{
    public interface ICRUD<T> where T: BaseEntity
    {
        void AddEntity(T entity);
        void EditEntity(T entity);
        T GetEntity(Guid Id);
        IEnumerable<T> GetAllEntity();
        void Delete(T entity);
    }
}
