using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LF.SysAdm.Domain.Repositorys;
using LF.SysAdm.Data.Context;
using LF.SysAdm.Data.Repositorys.Dapper;
using LF.SysAdm.Domain.UOW;
using LF.SysAdm.Data.UOW;
using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Enum;

namespace LF.SysAdm.TEST.RepositoryTest
{
    [TestClass]
    public class RepositoryEmployeeTest
    {
        IDbConnectionContext _context;
        IRepositoryEmployee _repoEmployee;
        IRepositoryAddress _repoAddress;
        IUnityOfWork _uow;

        [TestInitialize]
        public void Initialize()
        {
            _context = new DbContextDapper();
            _repoEmployee = new RepositoryEmployeeDapper(_context);
            _repoAddress = new RepositoryAddressDapper(_context);
            _uow = new UnityOfWorkDapper(_context);
        }

        [TestMethod]
        public void TesteRepsitoryEmployee()
        {
            Random rdm = new Random();
            var num = rdm.Next(0, 1000);
            Address address = new Address(
                "Rua testeAddress", 25, "TesteAddress", "TesteAddress", "TTestAddress", "TesteAddress", "78005-210");
            Employee emp = new Employee(
                "Carlos Pinheiro", EProfile.Salesman, EDepartment.Loja, "815.661.102-03", new DateTime(1979, 09, 01), address);

            _repoAddress.AddEntity(address);
            _repoEmployee.AddEntity(emp);
            _uow.Commit();

            var mod = _repoEmployee.GetEntity(emp.ID);
            mod.Edit("Antonio Carlos", EProfile.Admin, EDepartment.RH, $"815.661.{num}-03", new DateTime(1980, 10, 21));
            _repoEmployee.EditEntity(mod);
            _uow.Commit();

            var employee = _repoEmployee.GetEmployee(mod.ID);
            var employAddree = _repoEmployee.GetEmployeeWithAddress(employee.EmployeeId);
            var empDepart = _repoEmployee.GetEmployeesDepartement(mod.Department);
            var ListProfile = _repoEmployee.GetEmployeesProfile(EProfile.Admin);
            var ListDepart = _repoEmployee.GetEmployeesDepartement(EDepartment.Financeiro);
        }
    }
}
