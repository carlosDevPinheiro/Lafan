using LF.SysAdm.Data.Context.Map.Template;
using LF.SysAdm.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

namespace LF.SysAdm.Data.Context.Map
{
    public class CustomerMap : LafanTemplateMap<Customer>
    {
        protected override void ConfigBody()
        {
            Property(x => x.Document)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Name") { IsUnique = true }))
                 .HasColumnType("varchar")
                 .HasMaxLength(16)
                 .IsRequired();

            Property(x => x.DateBirthday)
                .IsRequired();

            Property(x => x.Phone)
                .HasColumnType("varchar")
                .HasMaxLength(14)
                .IsRequired();

            Property(x => x.Gender)
                    .IsRequired();

            Property(x => x.Document)
                .HasColumnType("varchar")
                .HasMaxLength(16)
                .IsRequired();

            Property(x => x.DateRegister)
              .IsRequired();

            Property(x => x.DateOfChange)
              .IsOptional();
            
            Ignore(x => x.UserId);
        }

        protected override void ConfigNameTable()
        {
            ToTable("Customer");
        }

        protected override void ConfigPrimaryKey()
        {
            HasKey(x => x.ID);
        }

        protected override void ConfigRelationship()
        {
            HasMany(x => x.Rel_AdressList)
                .WithOptional(x => x.Rel_Customer)
                .HasForeignKey(x => x.CustomerId)
                //.Map(x => x.MapKey("CustomerId"))                
                .WillCascadeOnDelete(true);

            HasRequired(x => x.Rel_User)
                .WithOptional()
                .Map(x => x.MapKey("UserId"));                
        }
    }
}
