using LF.SysAdm.Shared;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LF.SysAdm.Data.Context
{
    public class DbContextDapper : IDbConnectionContext
    {
        public static IDbConnection Connection;
        public static IDbTransaction Transaction;

        public  DbContextDapper()
        {
            Connection = new SqlConnection(RunTime.ConnectionString);
            Connection.Open();
            Transaction = Connection.BeginTransaction();
        }

        // no UnityOfWork setar null nas propriedades Connection e Transaction e fechar as conecçoes
    }
}
