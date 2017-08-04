using Dapper;
using LF.SysAdm.Data.Context;
using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Querys.Product;
using LF.SysAdm.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Data;

namespace LF.SysAdm.Data.Repositorys.Dapper
{
    public class RepositoryProductDapper : CRUDDapper<Product>, IRepositoryProduct
    {
        private readonly DbContextDapper _context;
        private string SqlCmd = string.Empty;
        public RepositoryProductDapper(IDbConnectionContext context) : base(context)
        {
            _context = (DbContextDapper)context;
        }

        public ProductQuery GetProduct(Guid Id)
        {
            var paramers = new DynamicParameters();
            paramers.Add("@ID", Id, DbType.Guid);

            return DbContextDapper.Transaction.Connection.
                QueryFirstOrDefault<ProductQuery>(
                "LF_GetProduct",
                param: paramers,
                commandType: CommandType.StoredProcedure,
                transaction: DbContextDapper.Transaction);

        }

        public IEnumerable<ProductFromWebQuery> GetProducts(bool active = true, int skip = 0, int take = 10)
        {
            var parames = new DynamicParameters();
            parames.Add("@ACTIVE", active, DbType.Boolean);
            parames.Add("@SKIP", skip, DbType.Int32);
            parames.Add("@TAKE", take, DbType.Int32);

            SqlCmd = "SELECT PD.[ID] AS [ProductId],PD.[Name],PD.[Description],PD.[Price],PD.[Image]" +
                " FROM [dbo].[Product] AS PD WHERE PD.[Active] = @ACTIVE" +
                " ORDER BY PD.[Name] OFFSET @SKIP ROWS FETCH NEXT @TAKE ROWS ONLY";

            return DbContextDapper.Transaction
                .Connection.Query<ProductFromWebQuery>(SqlCmd,param:parames, transaction: DbContextDapper.Transaction);           
        }

        public IEnumerable<ProductFromWebQuery> GetProductsByCategory(Guid catId)
        {
            SqlCmd = $"SELECT PD.[ID] AS [ProductId],PD.[Name],PD.[Description],PD.[Price],PD.[Image]" +
               $" FROM [dbo].[Product] AS PD WHERE PD.[CategoryId] = '{catId}'";

            return DbContextDapper.Transaction
                .Connection.Query<ProductFromWebQuery>(SqlCmd, transaction: DbContextDapper.Transaction);
        }

        public IEnumerable<ProductFromWebQuery> GetProductsByDescription(string description, int skip = 0, int take = 10)
        {
            SqlCmd = $"SELECT PD.[ID] AS [ProductId],PD.[Name],PD.[Description],PD.[Price],PD.[Image]" +
                $" FROM [dbo].[Product] AS PD WHERE  LTRIM(PD.[Description]) LIKE '{description}%' " +
                $" ORDER BY PD.[Name] OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY";

            return DbContextDapper.Transaction
                .Connection.Query<ProductFromWebQuery>(SqlCmd, transaction: DbContextDapper.Transaction);
        }       
    }
}
