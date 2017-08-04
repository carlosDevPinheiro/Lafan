using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Querys.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.SysAdm.Domain.Repositorys
{
    public interface IRepositoryCategory: ICRUD<Category>
    {
        IEnumerable<CategoryQuery> GetCategorysName(string name);
        CategoryQuery GetCategory(Guid Id);
        IEnumerable<CategoryQuery> GetCategorys();
    }
}
