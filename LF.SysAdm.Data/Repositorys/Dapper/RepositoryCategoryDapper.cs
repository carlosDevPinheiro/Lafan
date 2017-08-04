using Dapper;
using LF.SysAdm.Data.Context;
using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Querys.Category;
using LF.SysAdm.Domain.Repositorys;
using System;
using System.Collections.Generic;

namespace LF.SysAdm.Data.Repositorys.Dapper
{
    public class RepositoryCategoryDapper : CRUDDapper<Category>, IRepositoryCategory
    {
        private readonly DbContextDapper _context;
        private string SqlCmd = string.Empty;
        public RepositoryCategoryDapper(IDbConnectionContext context) : base(context)
        {
            _context = (DbContextDapper)context;
        }

        public CategoryQuery GetCategory(Guid Id)
        {
            SqlCmd = $"SELECT [ID] AS [CategoryId],[NameCategory],[DescriptionCategory],[DateRegister], [DateOfChange]" +
                $" FROM[dbo].[Category] WHERE[ID] = '{Id}'";
           return DbContextDapper.Transaction.Connection.QueryFirstOrDefault<CategoryQuery>(SqlCmd,transaction:DbContextDapper.Transaction);
        }

        public IEnumerable<CategoryQuery> GetCategorys()
        {
            SqlCmd = $"SELECT [ID] AS [CategoryId],[NameCategory],[DescriptionCategory],[DateRegister], [DateOfChange]" +
                 $" FROM [dbo].[Category] ";

            return DbContextDapper.Transaction.Connection.Query<CategoryQuery>(SqlCmd, transaction: DbContextDapper.Transaction);
        }

        public IEnumerable<CategoryQuery> GetCategorysName(string name)
        {
            SqlCmd = $"SELECT [ID] AS [CategoryId],[NameCategory],[DescriptionCategory],[DateRegister], [DateOfChange]" +
                $" FROM [dbo].[Category] WHERE [NameCategory] LIKE '%{name}%'";

            return DbContextDapper.Transaction.Connection.Query<CategoryQuery>(SqlCmd, transaction: DbContextDapper.Transaction);
        }
    }
}

 