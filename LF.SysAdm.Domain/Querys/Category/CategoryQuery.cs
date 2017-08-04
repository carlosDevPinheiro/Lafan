using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.SysAdm.Domain.Querys.Category
{
    public class CategoryQuery : BaseQuery
    {
        public Guid CategoryId { get; set; }
        public string NameCategory { get; set; }
        public string DescriptionCategory { get; set; }

    }
}
