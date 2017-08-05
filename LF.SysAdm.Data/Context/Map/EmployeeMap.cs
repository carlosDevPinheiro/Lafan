using LF.SysAdm.Data.Context.Map.Template;
using LF.SysAdm.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.SysAdm.Data.Context.Map
{
    public class EmployeeMap : LafanTemplateMap<Employee>
    {
        protected override void ConfigBody()
        {
            Property(x => x.Name)
                .HasMaxLength(80)
                .IsRequired();

            Property(x => x.Document)
               .HasMaxLength(16)
               .IsRequired();

            Property(x => x.DateBirthday)              
              .IsRequired();

            Property(x => x.DateRegister)              
              .IsRequired();

            Property(x => x.DateOfChange)
              .IsOptional();

            Property(x => x.RE)
                .HasMaxLength(20)
                .IsRequired();

            Ignore(x => x.AddressId);
        }

        protected override void ConfigNameTable()
        {
            ToTable("Employee");
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
