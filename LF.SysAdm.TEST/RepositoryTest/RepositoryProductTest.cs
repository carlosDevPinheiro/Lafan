using LF.SysAdm.Data.Context;
using LF.SysAdm.Data.Repositorys.Dapper;
using LF.SysAdm.Data.Repositorys.EF;
using LF.SysAdm.Data.UOW;
using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Repositorys;
using LF.SysAdm.Domain.UOW;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LF.SysAdm.TEST.RepositoryTest
{
    [TestClass]
    public class RepositoryProductTest
    {
        IDbConnectionContext _context;
        IRepositoryProduct _repositoryProd;
        IRepositoryCategory _repositoryCateg;
        IRepositorySupply _repositorySupp;
        IRepositoryAddress _repositoryAddress;
        IUnityOfWork _uow;
       
        [TestInitialize]
        public void Initialize()
        {
            //_context = new DbContextDapper();
            //_repositoryProd = new RepositoryProductDapper(_context);
            //_repositoryCateg = new RepositoryCategoryDapper(_context);
            //_repositorySupp = new RepositorySupplyDapper(_context);
            //_repositoryAddress = new RepositoryAddressDapper(_context);
            //_uow = new UnityOfWorkDapper(_context);

            _context = new DbContextEF();
            _repositoryProd = new RepositoryProductEF(_context);
            _repositoryCateg = new RepositoryCategoryEF(_context);
            _repositorySupp = new RepositorySupplyEF(_context);
            _repositoryAddress = new RepositoryAddressEF(_context);
            _uow = new UnityOfWorkEF(_context);
        }

        [TestMethod]
        [TestCategory("Repository - Product")]
        public void TestProduct()
        {
            Random rdm = new Random();
            var n = rdm.Next(0, 1000);

            Address address = new Address("Rua testeAddress", 25, "TesteTesteAddress", "TesteTesteAddress", "TesteTestAddress", "TesteTesteAddress", "78005-210");
            var supply = new Supply("SuplyName", $"75.{rdm.Next(100, 999)}.813/0001-00", "30526425", "AgenteSup", "emailsuply", address);

            var cat = new Category("Category"+n,"Descricao Teste"+n);
            Product prod = new Product(
                "ProductTest" + n, "Descricao Teste" + n, DateTime.Now.AddMonths(3), n, 3.0M, $"IMAGE{n}.jpg",
                $"2017{n * n}", n, cat, supply);

            _repositoryAddress.AddEntity(address);
                 
            _repositorySupp.AddEntity(supply);
            _repositoryCateg.AddEntity(cat);
            _repositoryProd.AddEntity(prod);
            _uow.Commit();

            var a = n * n;
            var product = _repositoryProd.GetEntity(prod.ID);
            product.UpDate(
                prod.Name + a, prod.Name + a, DateTime.Now.AddMonths(6), prod.Quantity + a, $"prod{a}.png", prod.Batch + a,
                prod.Invoice + a, prod.Price);
            _repositoryProd.EditEntity(product);
            _uow.Commit();

            var prodWeb = _repositoryProd.GetProduct(product.ID);
            var list = _repositoryProd.GetProducts();
            var list2 = _repositoryProd.GetProductsByCategory(cat.ID);
            var list3 = _repositoryProd.GetProductsByDescription(product.Description.Substring(0,5));
            var p = _repositoryProd.GetEntity(prodWeb.ProductId);
        }
    }
}
