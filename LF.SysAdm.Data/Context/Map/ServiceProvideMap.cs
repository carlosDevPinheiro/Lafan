using LF.SysAdm.Data.Context.Map.Template;
using LF.SysAdm.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.SysAdm.Data.Context.Map
{
    public class ServiceProvideMap : LafanTemplateMap<ServiceProvide>
    {
        protected override void ConfigBody()
        {
            Property(x => x.DateOfChanged)
                .IsOptional();

            Property(x => x.Tempo)
                .IsRequired();

            Property(x => x.Price)
                .IsRequired();

            Property(x => x.Description)
                .HasMaxLength(400)
                .IsOptional();

            Property(x => x.DateRegister)
                .IsRequired();

            Ignore(x => x.EmployeeId);
        }

        protected override void ConfigNameTable()
        {
            ToTable("ServiceProvide");
        }

        protected override void ConfigPrimaryKey()
        {
            HasKey(x => x.ID);
        }

        protected override void ConfigRelationship()
        {
            HasRequired(x => x.Rel_Employee)
                .WithOptional()
                .Map(x => x.MapKey("EmployeeId"));
        }
    }
}
