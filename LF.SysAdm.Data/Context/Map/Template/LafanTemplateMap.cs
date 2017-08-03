using System.Data.Entity.ModelConfiguration;

namespace LF.SysAdm.Data.Context.Map.Template
{
    public abstract class LafanTemplateMap<T> : EntityTypeConfiguration<T> where T : class
    {
        public LafanTemplateMap()
        {
            ConfigNameTable();
            ConfigPrimaryKey();
            ConfigBody();
            ConfigRelationship();

        }

        protected abstract void ConfigNameTable();
        protected abstract void ConfigPrimaryKey();
        protected abstract void ConfigBody();
        protected abstract void ConfigRelationship();
    }
}
