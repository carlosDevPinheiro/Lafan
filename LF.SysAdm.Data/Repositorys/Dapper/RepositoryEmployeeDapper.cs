using Dapper;
using LF.SysAdm.Data.Context;
using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Enum;
using LF.SysAdm.Domain.Querys.Employee;
using LF.SysAdm.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LF.SysAdm.Data.Repositorys.Dapper
{
    public class RepositoryEmployeeDapper : CRUDDapper<Employee>, IRepositoryEmployee
    {
        private readonly DbContextDapper _context;
        private string SqlCmd = String.Empty;
        public RepositoryEmployeeDapper(IDbConnectionContext context) : base(context)
        {
            _context = (DbContextDapper)context;
        }

        public EmployeeQuery GetEmployee(string RE)
        {
            SqlCmd = $"SELECT [ID] AS [EmployeeId],[Name],[Function],[Department],[Document],[DateBirthday],[DateRegister]," +
                $"[DateOfChange],[RE] FROM [dbo].[Employee] WHERE [RE] = '{RE}'";

            return DbContextDapper.Transaction
                .Connection.QueryFirstOrDefault<EmployeeQuery>(SqlCmd, transaction: DbContextDapper.Transaction);           
        }

        public EmployeeQuery GetEmployee(Guid Id)
        {
            SqlCmd = $"SELECT [ID] AS [EmployeeId],[Name],[Function],[Department],[Document],[DateBirthday],[DateRegister]," +
                $"[DateOfChange],[RE] FROM [dbo].[Employee] WHERE [ID] = '{Id}'";

            return DbContextDapper.Transaction
                 .Connection.QueryFirstOrDefault<EmployeeQuery>(SqlCmd, transaction: DbContextDapper.Transaction);
        }

        public IEnumerable<EmployeeQuery> GetEmployeesDepartement(EDepartment dep)
        {
            var parames = new DynamicParameters();
            parames.Add("@DEP", dep, DbType.Int32);

            SqlCmd = "SELECT [ID] AS [EmployeeId],[Name],[Function],[Department],[Document],[DateBirthday],[DateRegister]," +
                $"[DateOfChange],[RE] FROM [dbo].[Employee] WHERE [Department] = @DEP";

            return DbContextDapper.Transaction
                 .Connection.Query<EmployeeQuery>(SqlCmd,param:parames ,transaction: DbContextDapper.Transaction);
        }

        public IEnumerable<EmployeeQuery> GetEmployeesProfile(EProfile profile)
        {
            var parames = new DynamicParameters();
            parames.Add("@PROFILE", profile, DbType.Int32);

            SqlCmd = $"SELECT [ID] AS [EmployeeId],[Name],[Function],[Department],[Document],[DateBirthday],[DateRegister]," +
                $"[DateOfChange],[RE] FROM [dbo].[Employee] WHERE [Function] = @PROFILE";

            return DbContextDapper.Transaction
                 .Connection.Query<EmployeeQuery>(SqlCmd,param:parames ,transaction: DbContextDapper.Transaction);
        }

        public EmployeeWithAddressQuery GetEmployeeWithAddress(Guid Id)
        {
            SqlCmd = $"SELECT * FROM [LF_EmployeeWithAddress] WHERE [EmployeeId] = '{Id}'";
            return DbContextDapper.Transaction
                .Connection.QueryFirstOrDefault<EmployeeWithAddressQuery>(SqlCmd, transaction: DbContextDapper.Transaction);
        }
    }
}
