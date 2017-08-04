using Dapper;
using LF.SysAdm.Data.Context;
using LF.SysAdm.Domain.Entity.Base;
using LF.SysAdm.Domain.Querys;
using LF.SysAdm.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace LF.SysAdm.Data.Repositorys.Dapper
{
    public class CRUDDapper<T> : ICRUD<T> where T : BaseEntity
    {
        DbContextDapper _context;
        public CRUDDapper(IDbConnectionContext context)
        {
            _context = (DbContextDapper)context;
        }

        public void AddEntity(T entity)
        {

            var obj = typeof(T);
            var tableName = obj.Name;

            PropertyInfo[] props = entity.GetType().GetProperties();
            List<string> propertys = new List<string>();
            var param = new DynamicParameters();
            string name = string.Empty;
           
           

            foreach (var p in props)
            {
                if ((!p.Name.StartsWith("Rel_") && !p.Name.EndsWith("Id")) && p.PropertyType.Name != typeof(ICollection<>).Name)
                {
                    propertys.Add(p.Name);
                    param.Add("@" + p.Name, p.GetValue(entity, null));
                }
                else if (p.Name.StartsWith("Rel_"))
                {
                    name = p.Name.Substring(4) + "Id";
                    propertys.Add(name); 
                }
                else if(p.Name.EndsWith("Id"))
                {
                    param.Add("@" + p.Name, p.GetValue(entity, null));
                }
            }

            var columns = propertys.ToArray();

            var SqlCmd = string.Format("INSERT INTO [{0}] ([{1}]) VALUES (@{2})",
                tableName,
                string.Join("],[", columns),
                string.Join(",@", columns));

            DbContextDapper.Transaction.Connection.Execute(SqlCmd, param: param, transaction: DbContextDapper.Transaction);

        }

        public void Delete(T entity)
        {
            var obj = typeof(T);
            var tableName = obj.Name;

            string SqlCmd = $"DELETE FROM [{tableName}] WHERE [ID] = '{entity.ID}'";


            DbContextDapper.Transaction.Connection.Execute(SqlCmd, transaction: DbContextDapper.Transaction);

        }

        public void EditEntity(T entity)
        {
            var obj = typeof(T);
            var tableName = obj.Name;
            PropertyInfo[] props = entity.GetType().GetProperties();

            List<string> propertys = new List<string>();
            var param = new DynamicParameters();

            foreach (var p in props)
            {
                if (p.PropertyType.Name != typeof(ICollection<>).Name && !p.Name.StartsWith("Rel_"))
                {
                    propertys.Add("[" + p.Name + "]=@" + p.Name);
                    param.Add("@" + p.Name, p.GetValue(entity, null));
                }
            }

            string[] Colums = propertys.ToArray();
            var SqlCmd = string.Format("UPDATE [{0}] SET {1} WHERE [ID] = @ID", tableName, string.Join(",", Colums));

            DbContextDapper.Transaction.Connection.Execute(SqlCmd, param: param, transaction: DbContextDapper.Transaction);
        }

        public IEnumerable<T> GetAllEntity()
        {
            var obj = typeof(T);
            var tableName = obj.Name;

            string SqlCmd = $"SELECT * FROM [dbo].[{tableName}]";

            return DbContextDapper.Transaction.Connection.Query<T>(SqlCmd, transaction: DbContextDapper.Transaction);
        }

        public T GetEntity(Guid Id)
        {
            var obj = typeof(T);
            var tableName = obj.Name;

            string SqlCmd = $"SELECT * FROM [dbo].[{tableName}] WHERE [ID] = '{Id}'";


            var result = DbContextDapper.Transaction
                .Connection.QueryFirstOrDefault<T>(SqlCmd, transaction: DbContextDapper.Transaction);

            return result;
        }

    }
}
