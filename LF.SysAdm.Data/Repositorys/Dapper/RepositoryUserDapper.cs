using System;
using Dapper;
using LF.SysAdm.Data.Context;
using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Querys.User;
using LF.SysAdm.Domain.Repositorys;

namespace LF.SysAdm.Data.Repositorys.Dapper
{
    public class RepositoryUserDapper : CRUDDapper<Users>, IRepositoryUser
    {
        DbContextDapper _context;
        private string SqlCmd = string.Empty;
        public RepositoryUserDapper(IDbConnectionContext context) : base(context)
        {
            _context = (DbContextDapper)context;
        }

        public Users Authenticate(string email, string password)
        {
           SqlCmd = $"SELECT * FROM [dbo].[Users]  WHERE [Email] = '{email}' AND [Password] = '{password}'";

                return DbContextDapper.Transaction.Connection.QueryFirstOrDefault<Users>(SqlCmd, transaction: DbContextDapper.Transaction); ;
        }

        public UserQuery GetUser(Guid Id)
        {
            SqlCmd = $"SELECT * FROM [Users] WHERE [ID] = '{Id}'";
            return  DbContextDapper.Transaction.Connection.
                QueryFirstOrDefault<UserQuery>(SqlCmd, transaction: DbContextDapper.Transaction);
        }

        public Users GetUserEmail(string email)
        {
            SqlCmd = $"SELECT * FROM [Users] WHERE [Email] = '{email}'";           
            return DbContextDapper.Transaction.Connection.
                QueryFirstOrDefault<Users>(SqlCmd, transaction: DbContextDapper.Transaction);
        }

        public bool IsUserExist(string email)
        {
             SqlCmd = $"SELECT [Email] FROM  [Users] WHERE [Email] = '{email}'";
           
            var result = DbContextDapper.Transaction.Connection
                .QueryFirstOrDefault<Users>(SqlCmd, transaction: DbContextDapper.Transaction);

            if (result != null)
                return true;

            return false;
        }
    }
}
