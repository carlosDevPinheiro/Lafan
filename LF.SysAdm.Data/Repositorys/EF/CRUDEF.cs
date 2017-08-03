using LF.SysAdm.Data.Context;
using LF.SysAdm.Domain.Entity.Base;
using LF.SysAdm.Domain.Querys;
using LF.SysAdm.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LF.SysAdm.Data.Repositorys.EF
{
    public class CRUDEF<T> : ICRUD<T> where T: BaseEntity 
    {
        DbContextEF _context;
        public CRUDEF(IDbConnectionContext context)
        {
            _context = (DbContextEF)context;
        }
        public void AddEntity(T entity) => _context.Set<T>().Add(entity);
        public void Delete(T entity) => _context.Set<T>().Remove(entity);        

        public void EditEntity(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<T> GetAllEntity() => _context.Set<T>().AsNoTracking().ToList();
        public T GetEntity(Guid Id) => _context.Set<T>().Where(X => X.ID.Equals(Id)).FirstOrDefault();

        
    }
}
