using LF.SysAdm.Data.Context;
using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Querys.Category;
using LF.SysAdm.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LF.SysAdm.Data.Repositorys.EF
{
    public class RepositoryCategoryEF : CRUDEF<Category>, IRepositoryCategory
    {
        private readonly DbContextEF _context;
        public RepositoryCategoryEF(IDbConnectionContext context) : base(context)
        {
            _context = (DbContextEF)context;
        }

        public CategoryQuery GetCategory(Guid Id) => _context.Set<Category>().Where(x => x.ID == Id)
            .Select(x => new CategoryQuery { CategoryId = x.ID, NameCategory = x.NameCategory, DescriptionCategory = x.DescriptionCategory }).FirstOrDefault();

        public IEnumerable<CategoryQuery> GetCategorys()
        {
            return (from cat in _context.Set<Category>()                   
                    select new CategoryQuery
                    {
                        CategoryId = cat.ID,
                        NameCategory = cat.NameCategory,
                        DescriptionCategory = cat.DescriptionCategory

                    }).ToList();
        }

        public IEnumerable<CategoryQuery> GetCategorysName(string name)
        {
            return (from cat in _context.Set<Category>()
                    where cat.NameCategory.Contains(name)
                    select new CategoryQuery
                    {
                        NameCategory = cat.NameCategory,
                        DescriptionCategory = cat.DescriptionCategory

                    }).ToList();
        }
    }
}
