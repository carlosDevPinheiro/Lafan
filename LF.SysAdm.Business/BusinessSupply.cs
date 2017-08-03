using LF.SysAdm.Business.Base;
using LF.SysAdm.Domain.Business;
using LF.SysAdm.Domain.Command.Supply;
using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Querys;
using LF.SysAdm.Domain.Repositorys;
using LF.SysAdm.Domain.UOW;
using System;

namespace LF.SysAdm.Business
{
    public class BusinessSupply : BaseBusiness, IBusinessSupply
    {
        private readonly IRepositorySupply _repositorySupply;
        private readonly IRepositoryAddress _repositoryAddress;

        public BusinessSupply(IUnityOfWork uow, IRepositorySupply repository, IRepositoryAddress repositoryAddress)
            : base(uow)
        {
            _repositorySupply = repository;
            _repositoryAddress = repositoryAddress;            
        }

        public ObjectRequest EditeSupply(EditeSupplyCommand cmd)
        {
            var supply = _repositorySupply.GetEntity(cmd.SupplyId);
            if (supply == null)
                return new ObjectRequest().CreateObjectRequest("Fornecedor não esta cadastrado no banco",false);

            supply.Edite(cmd.CompanyName, cmd.CNPJ, cmd.Phone, cmd.Agent, cmd.Email);

            if (Commit(supply))
            {
                _repositorySupply.EditEntity(supply);
                return new ObjectRequest()
                    .CreateObjectRequest($" Dados do Fornecedor {supply.CompanyName} atualizado com Sucesso ", true);
               
            }
            return new ObjectRequest().CreateErrorNotification(supply.ListErrors());
        }

        public ObjectRequest GetAllSupplys()
        {
            return new ObjectRequest().CreateObjectRequest(_repositorySupply.GetSupplyes(),true);
        }

        public ObjectRequest GetSupply(Guid Id) => new ObjectRequest().CreateObjectRequest(_repositorySupply.GetSupply(Id), true);


        public ObjectRequest GetSupply(string cnpj) => new ObjectRequest()
            .CreateObjectRequest(_repositorySupply.GetSupplyCNPJ(cnpj), true);


        public ObjectRequest NewSupply(RegisterSupplyCommand cmd)
        {
           

            var supplyIsExist = _repositorySupply.GetSupplyCNPJ(cmd.CNPJ);
            if (supplyIsExist != null)
                return new ObjectRequest().CreateObjectRequest($" O CNPJ: {cmd.CNPJ} ja esta Cadastrado no Sistema ", false);
            Address address = new Address(cmd.Street, cmd.Number, cmd.Complement, cmd.District, cmd.City, cmd.State, cmd.CEP, null);
            Supply newSupply = new Supply(cmd.CompanyName, cmd.CNPJ, cmd.Phone, cmd.Agent, cmd.Email, address);


            _repositoryAddress.AddEntity(address);
            _repositorySupply.AddEntity(newSupply);

            address.ListErrors().ForEach((X) =>  newSupply.ListErrors().Add(X) );

            if (Commit(newSupply))
                return new ObjectRequest().CreateObjectRequest("Fornecedor Registrado com Sucesso", true);

                return new ObjectRequest().CreateErrorNotification(newSupply.ListErrors());
        }
    }
}
