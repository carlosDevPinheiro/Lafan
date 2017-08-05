using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LF.SysAdm.Data.Context;
using LF.SysAdm.Domain.Repositorys;
using LF.SysAdm.Data.Repositorys.Dapper;
using LF.SysAdm.Domain.UOW;
using LF.SysAdm.Data.UOW;

namespace LF.SysAdm.TEST.RepositoryTest
{
    [TestClass]
    public class RepositoryOrderTest
    {
        IDbConnectionContext _context;
        IRepositoryProduct _repositoryProd;
        IRepositoryCategory _repositoryCateg;
        IRepositorySupply _repositorySupp;
        IRepositoryAddress _repositoryAddress;
        IRepositoryUser _respositoryUser;
        IRepositoryCustomer _repositoryCustomer;
        IUnityOfWork _uow;

        [TestInitialize]
        public void Initialize()
        {
            _context = new DbContextDapper();
            _repositoryProd = new RepositoryProductDapper(_context);
            _repositoryCateg = new RepositoryCategoryDapper(_context);
            _repositorySupp = new RepositorySupplyDapper(_context);
            _repositoryAddress = new RepositoryAddressDapper(_context);            
            _respositoryUser = new RepositoryUserDapper(_context);
            _repositoryCustomer = new RepositoryCustomerDapper(_context);
            _uow = new UnityOfWorkDapper(_context);

            //_context = new DbContextEF();
            //_repositoryProd = new RepositoryProductEF(_context);
            //_repositoryCateg = new RepositoryCategoryEF(_context);
            //_repositorySupp = new RepositorySupplyEF(_context);
            //_repositoryAddress = new RepositoryAddressEF(_context);
            //_uow = new UnityOfWorkEF(_context);
        }

        [TestMethod]
        [TestCategory("Repository - Order")]
        public void TestOrder()
        {
            Random rdm = new Random();
            var n = rdm.Next(0, 1000);

            //TODO: Implemtar os TESTES
        }
    }
}
