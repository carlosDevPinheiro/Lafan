using LF.SysAdm.Data.Context.Map.Template;
using LF.SysAdm.Domain.Entity;

namespace LF.SysAdm.Data.Context.Map
{
    public class ProductMap : LafanTemplateMap<Product>
    {
        protected override void ConfigBody()
        {
            Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("varchar")
                .HasMaxLength(60)
                .IsRequired();

            Property(x => x.Description)
               .HasColumnName("Description")
               .HasColumnType("varchar")
               .HasMaxLength(100)
               .IsRequired();

            Property(x => x.DateExpiration)
              .HasColumnName("DateExpiration")
              .IsOptional();

            Property(x => x.Quantity)
              .HasColumnName("Quantity")
              .HasColumnType("int")
              .IsRequired();

            Property(x => x.Price)
               .HasColumnName("Price")
               .IsRequired();

            Property(x => x.Image)
              .HasColumnName("Image")
              .HasColumnType("varchar")
              .IsRequired();

            Property(x => x.Batch)
              .HasColumnName("Batch")
              .HasColumnType("varchar")
              .HasMaxLength(50)
              .IsRequired();

            Property(x => x.Invoice)
              .HasColumnName("Invoice")
              .HasColumnType("int")
              .IsRequired();

            Property(x => x.DateRegister)
              .HasColumnName("DateRegister")
              .IsRequired();

            Property(x => x.DateOfChange)
              .HasColumnName("DateOfChange")
              .IsOptional();

            Property(x => x.Active)
              .HasColumnName("Active")
              .IsRequired();

            Ignore(x => x.CategoryId);
            Ignore(x => x.SupplyId);
        }

        protected override void ConfigNameTable()
        {
            ToTable("Product");
        }

        protected override void ConfigPrimaryKey()
        {
            HasKey(x => x.ID);
        }

        protected override void ConfigRelationship()
        {
            HasRequired(x => x.Rel_Category)
                .WithOptional().Map(x => x.MapKey("CategoryId"));

            HasRequired(x => x.Rel_Supply)
                .WithOptional().Map(x => x.MapKey("SupplyId"));
        }
    }
}
