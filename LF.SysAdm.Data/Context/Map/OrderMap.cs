using LF.SysAdm.Data.Context.Map.Template;
using LF.SysAdm.Domain.Entity;

namespace LF.SysAdm.Data.Context.Map
{
    public class OrderMap : LafanTemplateMap<Order>
    {
        protected override void ConfigBody()
        {
            
            Property(x => x.OrderDate)
                .IsRequired();
            
            Property(x => x.ChangeDate)
                .IsOptional();
            
            Property(x => x.Status)
                .IsRequired();
           
            Property(x => x.Total)
                .IsRequired();
            
            Property(x => x.Comments)
                .HasMaxLength(400)
                .IsOptional();
          
            Property(x => x.PaymentMethod)
                .IsRequired();
           
            Property(x => x.Discount)
                .IsOptional();

            Ignore(x => x.CustomerId);
            Ignore(x => x.EmployeeId);
        }

        protected override void ConfigNameTable()
        {
            ToTable("Order");
        }

        protected override void ConfigPrimaryKey()
        {
            HasKey(x => x.ID);
        }

        protected override void ConfigRelationship()
        {
            HasRequired(x => x.Rel_Customer)
                 .WithOptional()
                 .Map(x => x.MapKey("CustomerId"));

            HasRequired(x => x.Rel_Employee)
                .WithOptional()
                .Map(x => x.MapKey("EmployeeId"));            
        }
    }
}
