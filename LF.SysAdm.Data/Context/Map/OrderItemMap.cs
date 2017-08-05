using LF.SysAdm.Data.Context.Map.Template;
using LF.SysAdm.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.SysAdm.Data.Context.Map
{
    public class OrderItemMap : LafanTemplateMap<OrderItem>
    {
        protected override void ConfigBody()
        {
            Property(x => x.ItemName)
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.Price)
                .IsRequired();

            Property(x => x.ProdId)
                .IsRequired();

            Property(x => x.Quantity)
                .IsRequired();

            Ignore(x => x.OrderId);
        }

        protected override void ConfigNameTable()
        {
            ToTable("OrderItem");
        }

        protected override void ConfigPrimaryKey()
        {
            HasKey(x => x.ID);
        }

        protected override void ConfigRelationship()
        {
            HasRequired(x => x.Rel_Order)
                .WithMany(x => x.ListItens)
                .Map(x => x.MapKey("OrderId"));
        }
    }
}
