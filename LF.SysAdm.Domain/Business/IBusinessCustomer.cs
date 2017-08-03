using LF.SysAdm.Domain.Command.Address;
using LF.SysAdm.Domain.Command.Customer;
using LF.SysAdm.Domain.Querys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.SysAdm.Domain.Business
{
    public interface IBusinessCustomer
    {
        ObjectRequest NewCustomer(RegisterCustomerCommand cmd);
        ObjectRequest EditeCustomer(EditCustomerCommand cmd);
        ObjectRequest RemoveCustomer(Guid Id);
        ObjectRequest GetCustomer(Guid Id);
        ObjectRequest GetCustomer(string cpf);
        ObjectRequest GetCustomerWithAddress(Guid Id);
        ObjectRequest GetCustomerWithUser(Guid Id);
        ObjectRequest NewAddressCustomer(RegisterAddressCommand cmd);
        ObjectRequest EditeAddressCustomer(EditeAddressCommand cmd);
    }
}
