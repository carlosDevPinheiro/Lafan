using LF.SysAdm.Domain.Entity.Base;
using LF.SysAdm.Domain.Enum;
using LF.SysAdm.Shared.Utils;
using LF.SysAdm.Shared.Validations;
using System;

namespace LF.SysAdm.Domain.Entity
{
    public class Employee : BaseEntity
    {
        protected Employee()  { }

        public Employee(string name, EProfile function, EDepartment dep, string doc, DateTime birthDay, Address addr)
        {
            Name = Helpers.Capitalize(name);
            Function = Function;
            Department = dep;
            Document = doc;
            DateBirthday = birthDay;
            RE = GeneratorRE();
            DateRegister = DateTime.Now;
            AddressId = addr.ID;
            Rel_Address = addr;
        }

        public string Name { get; private set; }
        public EProfile Function { get; private set; }
        public EDepartment Department { get; private set; }
        public string Document { get; private set; }
        public DateTime DateBirthday { get; private set; }        
        public DateTime DateRegister { get; private set; }
        public DateTime? DateOfChange { get; private set; }
        public string RE { get; private set; }

        public Guid AddressId { get; private set; }
        public Address Rel_Address { get; private set; }

        public override void Register()
        {
            new ValidationContract<Employee>(this)
                 .IsRequired(x => x.Name, "Nome do Funcionario é Obrigatorio")
                 .HasMaxLenght(x => x.Name, 80, "Tamanho maximo para Nome Funcionario é de 80 char")
                 .HasMinLenght(x => x.Name, 5, "Tamnho minimo para Nome do Funcionario é de 5 char")
                 .IsRequired(x => x.Document, "Documento  do Funcionario é Obrigatorio")
                 .IsFixedLenght(x => x.Document, 14, "Numero de documento invalido")
                 .IsLowerOrEqualsThan(x => x.DateBirthday, DateTime.Now);

        }

        public void Edit(string name, EProfile function, EDepartment dep, string doc, DateTime birthDay)
        {
            Name = Helpers.Capitalize(name);
            Function = Function;
            Department = dep;
            Document = doc;
            DateBirthday = birthDay;
            DateOfChange = DateTime.Now;

            new ValidationContract<Employee>(this)
                .IsRequired(x => x.Name, "Nome do Funcionario é Obrigatorio")
                .HasMaxLenght(x => x.Name, 80, "Tamanho maximo para Nome Funcionario é de 80 char")
                .HasMinLenght(x => x.Name, 5, "Tamnho minimo para Nome do Funcionario é de 5 char")
                .IsRequired(x => x.Document, "Documento  do Funcionario é Obrigatorio")
                .IsFixedLenght(x => x.Document, 14, "Numero de documento invalido")
                .IsLowerOrEqualsThan(x => x.DateBirthday, DateTime.Now);
        }

        private string GeneratorRE()
        {
            Random rdm = new Random();
            var numero = rdm.Next(0, 99);
            var Year = DateTime.Now.Year;
            var Dia = DateTime.Now.Day;
            var Hour = DateTime.Now.Hour;
            var Min = DateTime.Now.Minute;

            var result = $"{numero.ToString() + Year.ToString() + Dia.ToString() + Hour.ToString() + Min.ToString()}";

            return result;
        }
    }
}
