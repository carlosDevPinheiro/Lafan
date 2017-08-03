using LF.SysAdm.Data.Context.Map.Template;
using LF.SysAdm.Domain.Entity;
using System;

namespace LF.SysAdm.Data.Context.Map
{
    public class CategoryMap : LafanTemplateMap<Category>
    {
        protected override void ConfigBody()
        {
            Property(x => x.NameCategory)               
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            Property(x => x.DescriptionCategory)               
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.DateRegister)
                .HasColumnName("DateRegister")
                .IsRequired();

            Property(x => x.DateOfChange)
                .HasColumnName("DateOfChange")
                .IsOptional();
        }

        protected override void ConfigNameTable()
        {
            ToTable("Category");
        }

        protected override void ConfigPrimaryKey()
        {
            HasKey(x => x.ID);
        }

        protected override void ConfigRelationship()
        {
           //
        }
    }
}
