using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LF.SysAdm.Domain.Repositorys;
using LF.SysAdm.Data.Repositorys.Dapper;
using LF.SysAdm.Data.Context;
using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.UOW;
using LF.SysAdm.Data.UOW;

namespace LF.SysAdm.TEST.RepositoryTest
{
    [TestClass]
    public class RepositoryCategoryTest
    {
        private IRepositoryCategory _repository;
        private IDbConnectionContext _context;
        private IUnityOfWork _uow;

        [TestInitialize]
        public void Initialize()
        {
            _context = new DbContextDapper();
            _repository = new RepositoryCategoryDapper(_context);
            _uow = new UnityOfWorkDapper(_context);
        }

        [TestMethod]
        [TestCategory("Repository - Category")]
        public void TesteRepository()
        {
            Random rdm = new Random();
            Category cat = new Category("Teste"+ rdm.Next(0,1000), "Todas marcas mais famosas");

            _repository.AddEntity(cat);
            _uow.Commit();
            var editCat = _repository.GetEntity(cat.ID);
            editCat.UpDate("Categoria"+ rdm.Next(0,2000), "Teste Nova Categoria");            
            _repository.EditEntity(editCat);
            _uow.Commit();
            var catQuery = _repository.GetCategory(editCat.ID);
            var list = _repository.GetAllEntity();
            var list2 = _repository.GetCategorysName("Teste");
        }
    }
}
