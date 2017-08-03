using LF.SysAdm.Data.Context.Map.Template;
using LF.SysAdm.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

namespace LF.SysAdm.Data.Context.Map
{
    public class SupplyMap : LafanTemplateMap<Supply>
    {
        protected override void ConfigBody()
        {
            Property(x => x.CompanyName)
                  .HasColumnName("CompanyName")
                  .HasColumnType("varchar")
                  .HasMaxLength(50)
                  .IsRequired();

            Property(x => x.Email)
                .HasColumnName("Email")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.CNPJ)
                .HasColumnAnnotation("CNPJ_IX", new IndexAnnotation(new IndexAttribute { IsUnique = true }))
                   .HasColumnName("CNPJ")
                   .HasColumnType("varchar")
                   .HasMaxLength(18)
                   .IsRequired();

            Property(x => x.Phone)
                  .HasColumnName("Phone")
                  .HasColumnType("varchar")
                  .HasMaxLength(14)
                  .IsRequired();

            Property(x => x.Agent)
                  .HasColumnName("Agent")
                  .HasColumnType("varchar")
                  .HasMaxLength(40)
                  .IsRequired();

            Property(x => x.DateRegister)
                 .HasColumnName("DateRegister")
                 .IsRequired();

            Property(x => x.DateOfChange)
                 .HasColumnName("DateOfChange")
                 .IsOptional();

            Ignore(x => x.AddressId);
        }

        protected override void ConfigNameTable()
        {
            ToTable("Supply");
        }

        protected override void ConfigPrimaryKey()
        {
            HasKey(x => x.ID);
        }

        protected override void ConfigRelationship()
        {
            HasRequired(x => x.Rel_Address)
               .WithOptional()
               .Map(x => x.MapKey("AddressId"));
        }
    }
}
