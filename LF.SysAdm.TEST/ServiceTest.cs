using LF.SysAdm.Business;
using LF.SysAdm.Data.Context;
using LF.SysAdm.Data.Repositorys.Dapper;
using LF.SysAdm.Data.Repositorys.EF;
using LF.SysAdm.Data.UOW;
using LF.SysAdm.Domain.Business;
using LF.SysAdm.Domain.Command.User;
using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Querys.Address;
using LF.SysAdm.Domain.Querys.Supply;
using LF.SysAdm.Domain.Querys.User;
using LF.SysAdm.Domain.Repositorys;
using LF.SysAdm.Domain.UOW;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LF.SysAdm.TEST
{
    [TestClass]
    public class ServiceTest
    {
        IDbConnectionContext _context;
        IRepositoryCustomer _repositoryCustomer;
        ICRUD<Users> _repositoryUsers;
        IRepositorySupply _repositorySupply;
        ICRUD<Address> _repositoryAddress;


        [TestInitialize]
        public void Initialize()
        {
            _context = new DbContextDapper();
            _repositoryCustomer = new RepositoryCustomerDapper(_context);
            _repositoryUsers = new CRUDDapper<Users>(_context);
            _repositorySupply = new RepositorySupplyDapper(_context);
            _repositoryAddress = new CRUDDapper<Address>(_context);

            //_context = new DbContextEF();
            //_repositoryCustomer = new RepositoryCustomerEF(_context);
            //_repositoryUsers = new CRUDEF<Users,UserQuery>(_context);
            //_repositorySupply = new RepositorySupplyEF(_context);
            //_repositoryAddress = new CRUDEF<Address, AddressQuery>(_context);
        }

        [TestMethod]
        public void RepositorioCustomer()
        {

            Random rdm = new Random();
            Users _user = new Users("Latino", $"latino{rdm.Next(0, 10000)}@gmail.com", "123456");
            

            Customer _customer = new Customer($"{rdm.Next(100, 999)}56811892", DateTime.Now, "30526425", true, _user);           
            Address _address = new Address("Rua testeAddress", 25, "TesteTesteAddress", "TesteTesteAddress", "TesteTestAddress", "TesteTesteAddress", "78005-210",_customer.ID);
           // _customer.Rel_AdressList.Add(_address);

            _repositoryUsers.AddEntity(_user);
            //((DbContextEF)_context).SaveChanges();
            _repositoryCustomer.AddEntity(_customer);
            //((DbContextEF)_context).SaveChanges();
            _repositoryAddress.AddEntity(_address);
            //((DbContextEF)_context).SaveChanges();
            // _repositoryCustomer.AddEntity(_customer);

            var customerQuery = _repositoryCustomer.GetCustomer(_customer.ID);
            var customerCPF = _repositoryCustomer.GetCustomerCPF(_customer.Document);
            var customerWithAddress = _repositoryCustomer.GetCustomerWithAddress(customerCPF.CustomerId);
            //Guid.Parse("7E03E3AA-9BB9-4BF6-9C4C-5BD584E8B514")
        }

        [TestMethod]
        [Ignore]
        public void TestMethod1()
        {
            IDbConnectionContext _context = new DbContextDapper();
            IRepositoryUser _repository = new RepositoryUserDapper(_context);
            IUnityOfWork _uow = new UnityOfWorkDapper(_context);
           // IBusinessUser _service = new BusinessUser(_repository, _uow);
            //Random rdm = new Random();
            //RegisterUsersCommand cmd = new RegisterUsersCommand
            //{
            //    Name = $"Carlos Pinheiro {rdm.Next(0, 1000)}",
            //    Email = $"carlos{rdm.Next(0, 1000)}@gmail.com",
            //    Password = "123456",
            //    ConfirmPassword = "123456"
            //};

            //var novo = _service.NewUser(cmd);

            //var ID = new Guid();/*(Guid)novo.DataResultObject;*/



            //EditUserCommand cmdUp = new EditUserCommand
            //{
            //    ID = ID,
            //    Name = "Antonio Carlos Pinheiro",
            //    Email = $"antoniocarlos{rdm.Next(0, 1000)}@gmail.com",
            //    ConfirmPassword = "888777",
            //    Password = "888777"
            //};

            //var userUpdate = _service.UpdateUser(cmdUp);
            ////var result = _service.Remove(((Users)userUpdate).ID);
        }

        [TestMethod]
        [Ignore]
        public void Teste()
        {
            Random rdm = new Random();

            Users _user = new Users("Latino", $"latino{rdm.Next(0, 10000)}@gmail.com", "123456");
            Address _address = new Address("Rua teste", 25, "TesteTeste", "TesteTeste", "TesteTest", "TesteTeste", "78005-210",null);

            Customer _customer = new Customer($"{rdm.Next(100, 999)}56811892", DateTime.Now, "30526425", true, _user);
            _customer.Rel_AdressList.Add(_address);

            _repositoryUsers.AddEntity(_user);
            ((DbContextEF)_context).SaveChanges();


            _repositoryCustomer.AddEntity(_customer);
            ((DbContextEF)_context).SaveChanges();

            var cust = _repositoryCustomer.GetEntity(_customer.ID);
            cust.Edit($"{rdm.Next(100, 999)}78965807", DateTime.Now, "(11)55601730", false);
            _repositoryCustomer.EditEntity(cust);
            ((DbContextEF)_context).SaveChanges();

            var CustMod = _repositoryCustomer.GetEntity(cust.ID);

            var todos = _repositoryCustomer.GetAllEntity();
            Address _addressSupply = new Address("Rua testeSupply", 25, "TesteTestetesteSupply", "TesteTestetesteSupply", "TesteTesttesteSupply", "TesteTestetesteSupply", "78005-210",null);
            var supply = new Supply("SuplyName", $"75.{rdm.Next(100, 999)}.813/0001-00", "30526425", "AgenteSup", "emailsuply", _addressSupply);

            _repositoryAddress.AddEntity(_addressSupply);
            ((DbContextEF)_context).SaveChanges();

            _repositorySupply.AddEntity(supply);
            ((DbContextEF)_context).SaveChanges();
        }

        [TestMethod]
        public void Supply()
        {
            Random rdm = new Random();
            Address _addressSupply = new Address("Rua testeSupply1", 25, "TesteTestetesteSupply1", "TesteTestetesteSupply1", "TesteTesttesteSupply1", "TesteTestetesteSupply1", "78005-210",null);
            var supply = new Supply("SuplyName", $"75.{rdm.Next(100, 999)}.813/0001-00", "30526425", "AgenteSup", "emailsuply", _addressSupply);

            _repositoryAddress.AddEntity(_addressSupply);
            // (_context as DbContextEF).SaveChanges();
            _repositorySupply.AddEntity(supply);
            // (_context as DbContextEF).SaveChanges();
            var resp = _repositorySupply.GetSupplyCNPJ(supply.CNPJ);
            var SupplyAddress = _repositorySupply.GetSupplyWithAddress(supply.ID);

        }
    }
}
