using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Enum;
using LF.SysAdm.Domain.Querys.Employee;
using System;
using System.Collections.Generic;

namespace LF.SysAdm.Domain.Repositorys
{
    public interface IRepositoryEmployee: ICRUD<Employee>
    {
        EmployeeQuery GetEmployee(string RE);
        EmployeeQuery GetEmployee(Guid Id);
        EmployeeWithAddressQuery GetEmployeeWithAddress(Guid Id);
        IEnumerable<EmployeeQuery> GetEmployeesDepartement(EDepartment dep);
        IEnumerable<EmployeeQuery> GetEmployeesProfile(EProfile profile);
    }
}
