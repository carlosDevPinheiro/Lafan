using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Querys.Product;
using System;
using System.Collections.Generic;

namespace LF.SysAdm.Domain.Repositorys
{
    public interface IRepositoryProduct: ICRUD<Product>
    {
        IEnumerable<ProductFromWebQuery> GetProductsByCategory(Guid catId);
        ProductQuery GetProduct(Guid Id);
        IEnumerable<ProductFromWebQuery> GetProductsByDescription(string description, int skip = 0, int take = 10);
        IEnumerable<ProductFromWebQuery> GetProducts(bool active = true, int skip = 0, int take = 10);
    }
}
