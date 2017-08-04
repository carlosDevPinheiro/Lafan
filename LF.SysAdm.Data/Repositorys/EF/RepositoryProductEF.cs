using LF.SysAdm.Data.Context;
using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Querys.Product;
using LF.SysAdm.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LF.SysAdm.Data.Repositorys.EF
{
    public class RepositoryProductEF : CRUDEF<Product>, IRepositoryProduct
    {
        private readonly DbContextEF _context;
        public RepositoryProductEF(IDbConnectionContext context) : base(context)
        {
            _context = (DbContextEF)context;
        }

        public ProductQuery GetProduct(Guid Id)
        {
            return (from prod in _context.Set<Product>()
                    join cat in _context.Set<Category>() on prod.Rel_Category.ID equals cat.ID
                    join sup in _context.Set<Supply>() on prod.Rel_Supply.ID equals sup.ID
                    where prod.ID == Id
                    select new ProductQuery
                    {
                        ProductId = prod.ID,
                        Name = prod.Name,
                        Description = prod.Description,
                        Price = prod.Price,
                        Quantity = prod.Quantity,
                        Image = prod.Image,
                        SupplyId = sup.ID,
                        CompanyName = sup.CompanyName,
                        CNPJ = sup.CNPJ,
                        CategoryId = cat.ID,
                        NameCategory = cat.NameCategory

                    }).FirstOrDefault();
        }

        public IEnumerable<ProductFromWebQuery> GetProducts(bool active = true, int skip = 0, int take = 10)
        {
            return (from prod in _context.Set<Product>()
                    where prod.Active.Equals(active)
                    select new ProductFromWebQuery
                    {
                        ProductId = prod.ID,
                        Description = prod.Description,
                        Image = prod.Image,
                        Name = prod.Image,
                        Price = prod.Price

                    }).OrderBy(x => x.Name).Skip(skip).Take(take).ToList();
        }

        public IEnumerable<ProductFromWebQuery> GetProductsByCategory(Guid catId)
        {
            return (from prod in _context.Set<Product>()
                    where prod.Rel_Category.ID == catId
                    select new ProductFromWebQuery
                    {
                        Description = prod.Description,
                        Image = prod.Image,
                        Name = prod.Name,
                        Price = prod.Price,
                        ProductId = prod.ID
                    }).ToList();
        }

        public IEnumerable<ProductFromWebQuery> GetProductsByDescription(string description, int skip = 0, int take = 10)
        {
            return _context.Set<Product>().OrderBy(x => x.Name)
                .Where(x => x.Description.Contains(description))
                .Select(x => new ProductFromWebQuery
                {
                    Description = x.Description,
                    Image = x.Image,
                    Name = x.Name,
                    Price = x.Price,
                    ProductId = x.ID

                }).OrderBy(x => x.Name).Skip(skip).Take(take).ToList();
        }
    }
}
