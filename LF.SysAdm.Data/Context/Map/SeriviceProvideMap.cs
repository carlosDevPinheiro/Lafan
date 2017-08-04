using LF.SysAdm.Data.Context.Map.Template;
using LF.SysAdm.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.SysAdm.Data.Context.Map
{
    public class SeriviceProvideMap : LafanTemplateMap<ServiceProvide>
    {
        protected override void ConfigBody()
        {
            //.IsGreaterOrEqualsThan(x => x.Tempo, 5, "Tempo minimo para um Servico é de 5 min")
            //    .IsGreaterOrEqualsThan(x => x.Price, 0.0m, "Valor não pode ser negativ ")
            //    .HasMaxLenght(x => x.Description, 400, "Tamnho Maximo para campo descrção é de 400 char");

            // TODO: IMPLEMENTAR

            throw new NotImplementedException();
        }

        protected override void ConfigNameTable()
        {
            throw new NotImplementedException();
        }

        protected override void ConfigPrimaryKey()
        {
            throw new NotImplementedException();
        }

        protected override void ConfigRelationship()
        {
            throw new NotImplementedException();
        }
    }
}
