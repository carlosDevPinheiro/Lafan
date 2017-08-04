using LF.SysAdm.Business.Base;
using LF.SysAdm.Domain.Business;
using LF.SysAdm.Domain.Command.Address;
using LF.SysAdm.Domain.Command.Customer;
using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Querys;
using LF.SysAdm.Domain.Repositorys;
using LF.SysAdm.Domain.UOW;
using System;

namespace LF.SysAdm.Business
{
    public class BusinessCustomer : BaseBusiness, IBusinessCustomer
    {
        private readonly IRepositoryCustomer _repCustomer;
        private readonly IRepositoryAddress _repAddress;
        private readonly IRepositoryUser _repUser;
        public BusinessCustomer(IUnityOfWork uow, IRepositoryCustomer repositoryCustomer, IRepositoryAddress repositoryAddress, IRepositoryUser repUser)
            : base(uow)
        {
            _repAddress = repositoryAddress;
            _repCustomer = repositoryCustomer;
            _repUser = repUser;
        }

        public ObjectRequest EditeAddressCustomer(EditeAddressCommand cmd)
        {
            var address = _repAddress.GetEntity(cmd.AddressId);
            if (address == null) return new ObjectRequest().CreateObjectRequest("Endereco nao Registrado", false);

            address.Edit(cmd.Street, cmd.Number, cmd.Complement, cmd.District, cmd.City, cmd.State, cmd.CEP);

            if (Commit(address))
                return new ObjectRequest().CreateObjectRequest("Endereco Atualizado com Sucesso", true);

            return new ObjectRequest().CreateErrorNotification(address.ListErrors());
           
        }

        public ObjectRequest EditeCustomer(EditCustomerCommand cmd)
        {
            var customer = _repCustomer.GetEntity(cmd.CustomerId);
            if (customer == null)
                return new ObjectRequest()
                    .CreateObjectRequest($"Cliente {cmd.Document} não registrado no Sistema ! ", false); 

               customer.Edit(cmd.Document, cmd.DateBirthday, cmd.Phone, cmd.Gender);

            _repCustomer.EditEntity(customer);

            if (Commit(customer))
                return new ObjectRequest().CreateObjectRequest($"Dados do Cliente {cmd.Document} foram alterados com sucesso !", true);

            return new ObjectRequest().CreateErrorNotification(customer.ListErrors()); 
        }

        public ObjectRequest GetCustomer(Guid Id) => new ObjectRequest()
            .CreateObjectRequest(_repCustomer.GetCustomer(Id),true);


        public ObjectRequest GetCustomer(string cpf) => new ObjectRequest()
            .CreateObjectRequest(_repCustomer.GetCustomerCPF(cpf),true); 


        public ObjectRequest GetCustomerWithAddress(Guid Id) => new ObjectRequest()
        .CreateObjectRequest( _repCustomer.GetCustomerWithAddress(Id),true);

        public ObjectRequest GetCustomerWithUser(Guid Id) => new ObjectRequest()
            .CreateObjectRequest(_repCustomer.GetCutomerWithUser(Id),true);

        public ObjectRequest NewAddressCustomer(RegisterAddressCommand cmd)
        {
            var customer = _repCustomer.GetCustomer(cmd.CustomerId);
            if (customer == null)
                return new ObjectRequest()
                    .CreateObjectRequest($"Endereço nao foi adicionado, Cliente não Registrado no Sistema", false);

            var address = new Address(cmd.Street, cmd.Number, cmd.Complement, cmd.District, cmd.City, cmd.State, cmd.CEP);

            _repAddress.AddEntity(address);

            if (Commit(address))
                 return new ObjectRequest().CreateObjectRequest($" Novo Endereco Add com Sucesso", true);

            return new ObjectRequest().CreateErrorNotification(address.ListErrors());
        }

        public ObjectRequest NewCustomer(RegisterCustomerCommand cmd)
        {
            var user = _repUser.GetUserEmail(cmd.Email);
            if (user == null)
                return new ObjectRequest().CreateObjectRequest($"Usuario nao encontrado", false);

            Customer _customer = new Customer(cmd.Document, cmd.DateBirthday, cmd.Phone, cmd.Gender, user);
            Address _address = new Address(cmd.Street, cmd.Number, cmd.Complement, cmd.District, cmd.City, cmd.State, cmd.CEP, _customer);

            _repCustomer.AddEntity(_customer);
            _repAddress.AddEntity(_address);
           

            _address.ListErrors().ForEach(x => _customer.ListErrors().Add(x));

            if (Commit(_customer))
                return new ObjectRequest().CreateObjectRequest($"Cliente {_customer.Document} Registrado com Sucesso ", true);

            return new ObjectRequest().CreateErrorNotification(_customer.ListErrors());
        }

        public ObjectRequest RemoveCustomer(Guid Id)
        {
            var customer = _repCustomer.GetEntity(Id);
            _repCustomer.Delete(customer);

            if (Commit(customer))
              return new ObjectRequest().CreateObjectRequest("Cliente removido :( ", true);
          
            return new ObjectRequest().CreateObjectRequest($"Não foi Possivel remover o cliente {customer.Document}",false);
        }
    }
}
