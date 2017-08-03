using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Querys.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.SysAdm.Domain.Repositorys
{
    public interface IRepositoryCustomer: ICRUD<Customer>
    {   
        CustomerQuery GetCustomer(Guid Id);
        CustomerQuery GetCustomerCPF(string cpf);
        CustomerWithUserQuery GetCutomerWithUser(Guid Id);
        CustomerWithAddressQuery GetCustomerWithAddress(Guid Id);

    }
}
