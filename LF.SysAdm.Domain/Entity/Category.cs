using LF.SysAdm.Domain.Entity.Base;
using LF.SysAdm.Shared.Utils;
using LF.SysAdm.Shared.Validations;
using System;

namespace LF.SysAdm.Domain.Entity
{
    public class Category : BaseEntity
    {
        protected Category() { }

        public Category(string name, string desc)
        {
            NameCategory = Helpers.Capitalize(name);
            DescriptionCategory = desc;
            DateRegister = DateTime.Now;
            Register();
        }

        public string NameCategory { get; private set; }
        public string DescriptionCategory { get; private set; }
        public DateTime DateRegister { get; private set; }
        public DateTime? DateOfChange { get; private set; }

        public void UpDate(string name, string desc)
        {
            NameCategory = Helpers.Capitalize(name);
            DescriptionCategory = desc;

            new ValidationContract<Category>(this)
                .IsRequired(x => x.NameCategory, "Campo categoria é obrigatorio")
                .HasMaxLenght(x => x.NameCategory, 50, "Nome de Categoria no maximo 40 char")
                .HasMinLenght(x => x.NameCategory, 5, "Nome categoria deve ter no minimo 5 char")
                .IsRequired(x => x.DescriptionCategory, "Campo Dercricao categoria é obrigatorio")
                .HasMaxLenght(x => x.DescriptionCategory, 100, "Dercricao de Categoria no maximo 40 char")
                .HasMinLenght(x => x.DescriptionCategory, 10, "Descriacao categoria deve ter no minimo 5 char");

            DateOfChange = DateTime.Now;
        }

        public override void Register()
        {
            new ValidationContract<Category>(this)
                .IsRequired(x => x.NameCategory, "Campo categoria é obrigatorio")
                .HasMaxLenght(x => x.NameCategory, 50, "Nome de Categoria no maximo 40 char")
                .HasMinLenght(x => x.NameCategory, 5, "Nome categoria deve ter no minimo 5 char")
                .IsRequired(x => x.DescriptionCategory, "Campo Dercricao categoria é obrigatorio")
                .HasMaxLenght(x => x.DescriptionCategory, 100, "Dercricao de Categoria no maximo 40 char")
                .HasMinLenght(x => x.DescriptionCategory, 10, "Descriacao categoria deve ter no minimo 5 char");
        }
    }
}
