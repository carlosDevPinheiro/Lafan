using LF.SysAdm.Data.Context.Map.Template;
using LF.SysAdm.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.SysAdm.Data.Context.Map
{
    public class AddressMap : LafanTemplateMap<Address>
    {
        protected override void ConfigBody()
        {
            Property(x => x.Street)
                 .HasColumnType("varchar")
                 .HasMaxLength(80)
                 .IsRequired();

            Property(x => x.Number)
                .HasColumnType("int")
                .IsRequired();

            Property(x => x.Complement)
                .HasColumnType("varchar")
                .HasMaxLength(80)
                .IsRequired();

            Property(x => x.District)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            Property(x => x.City)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            Property(x => x.State)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            Property(x => x.CEP)
                .HasColumnType("varchar")
                .HasMaxLength(9)
                .IsRequired();

            Property(x => x.DateRegister)
             .IsRequired();

            Property(x => x.DateOfChange)
                .IsOptional();
        }

        protected override void ConfigNameTable()
        {
            ToTable("Address");
        }

        protected override void ConfigPrimaryKey()
        {
            HasKey(x => x.ID);
        }

        protected override void ConfigRelationship()
        {
           // HasRequired(x => x.Rel_Supply).
        }


        //TODO:  ARRUMAR RELACIONAMENTO SUPPLY COM ADDRESS A TABELA SUPPLY DEVE FICAR COM ACHAVE ESTRNAGEIRA DE ADDRESS
    }
}
