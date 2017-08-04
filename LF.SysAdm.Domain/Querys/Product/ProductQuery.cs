using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.SysAdm.Domain.Querys.Product
{
    public class ProductQuery : BaseQuery
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }        
        public int Quantity { get; set; }
        public decimal Price { get; set; }        
        public string Image { get; set; }

        public Guid CategoryId { get; set; }
        public String NameCategory { get; set; }
        public Guid SupplyId { get; set; }
        public string CompanyName { get; set; }
        public string CNPJ { get; set; }

    }
}
