using LF.SysAdm.Data.Context.Map.Template;
using LF.SysAdm.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

namespace LF.SysAdm.Data.Context.Map
{
    public class UserMap : LafanTemplateMap<Users>
    {
        protected override void ConfigBody()
        {
            Property(x => x.Name)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.Email)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Name") { IsUnique = true }))
                .IsRequired();

            Property(x => x.Password)
                .HasColumnType("varchar")
                .HasMaxLength(32)
                .IsRequired();

            Property(x => x.RegistrationDate)
                .IsRequired();

            Property(x => x.DateofChange)
                .IsOptional();

            Property(x => x.Active)
                .IsRequired();

            Property(x => x.Profile)
                .IsRequired();

        }

        protected override void ConfigNameTable()
        {
            ToTable("Users");
        }

        protected override void ConfigPrimaryKey()
        {
            HasKey(x => x.ID);
        }

        protected override void ConfigRelationship()
        {
            //HasOptional(X => X.Customer)
            //    .WithOptionalPrincipal(X => X.User)
            //    .WillCascadeOnDelete(true);
        }
    }
}
