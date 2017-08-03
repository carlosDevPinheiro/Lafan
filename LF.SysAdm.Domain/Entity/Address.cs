using LF.SysAdm.Domain.Entity.Base;
using LF.SysAdm.Shared.Utils;
using LF.SysAdm.Shared.Validations;
using System;

namespace LF.SysAdm.Domain.Entity
{
    public class Address : BaseEntity
    {
        protected Address() { }

        public Address(
            string street, int number, string compl, string dist, string city, string state, string cep, Guid? customerId)
        {
            this.Street = Helpers.Capitalize(street);
            this.Number = number;
            this.Complement = Helpers.Capitalize(compl);
            this.District = Helpers.Capitalize(dist);
            this.City = Helpers.Capitalize(city);
            this.State = Helpers.Capitalize(state);
            this.CEP = cep;
            this.DateRegister = DateTime.Now;
            this.CustomerId = customerId;
            this.Register();
        }

        public string Street { get; private set; }
        public int Number { get; private set; } = 0;
        public string Complement { get; private set; }
        public string District { get; private set; } 
        public string City { get; private set; }
        public string State { get; private set; }
        public string CEP { get; private set; } 
        public DateTime DateRegister { get; private set; }
        public DateTime? DateOfChange { get; private set; }

        public Guid? CustomerId { get; private set; }
        public Customer Rel_Customer { get; private set; }

      
        public override void Register()
        {
                 new ValidationContract<Address>(this)
                .HasMaxLenght(x => x.Street, 80, "Rua deve conter maximo 80 char")
                .HasMinLenght(x => x.Street, 5, "Rua invalida")
                .IsRequired(x => x.Street,"Campo Rua é obrigatorio")
                .HasMaxLenght(x => x.Complement, 80, "Complemento deve conter maximo 80 char")
                .IsRequired(x => x.Complement, "Campo Complemento é obrigatorio")
                .HasMaxLenght(x => x.District, 50, "Bairro deve conter maximo 50 char")
                .HasMinLenght(x => x.District, 5, "Bairro invalido")
                .IsRequired(x => x.District, "Campo Bairro é obrigatorio")
                .HasMaxLenght(x => x.City, 50, "Cidade deve conter maximo 50 char")
                .HasMinLenght(x => x.City, 3, "Cidade invalida")
                .IsRequired(x => x.City, "Campo Cidade é obrigatorio")
                .HasMaxLenght(x => x.State, 50, "Estado deve conter maximo 50 char")
                .HasMinLenght(x => x.State, 2, "Rua invalida")
                .IsRequired(x=> x.State,"Estado não foi informado")
                .IsFixedLenght(x=> x.CEP,9,"Cep invalido ou falta o '-'")
                .IsRequired(x => x.CEP,"Cep não Informado")
                .IsGreaterThan(x => x.Number, 1, "Numero deve ser maior que zero");

        }

        public void Edit(string street, int number, string compl, string dist, string city, string state, string cep)
        {
            this.Street = Helpers.Capitalize(street);
            this.Number = number;
            this.Complement = Helpers.Capitalize(compl);
            this.District = Helpers.Capitalize(dist);
            this.City = Helpers.Capitalize(city);
            this.State = Helpers.Capitalize(state);
            this.CEP = cep;
            this.DateOfChange = DateTime.Now;

            new ValidationContract<Address>(this)
                .HasMaxLenght(x => x.Street, 80, "Rua deve conter maximo 80 char")
                .HasMinLenght(x => x.Street, 5, "Rua invalida")
                .IsRequired(x => x.Street, "Campo Rua é obrigatorio")
                .HasMaxLenght(x => x.Complement, 80, "Complemento deve conter maximo 80 char")
                .IsRequired(x => x.Complement, "Campo Complemento é obrigatorio")
                .HasMaxLenght(x => x.District, 50, "Bairro deve conter maximo 50 char")
                .HasMinLenght(x => x.District, 5, "Bairro invalido")
                .IsRequired(x => x.District, "Campo Bairro é obrigatorio")
                .HasMaxLenght(x => x.City, 50, "Cidade deve conter maximo 50 char")
                .HasMinLenght(x => x.City, 3, "Cidade invalida")
                .IsRequired(x => x.City, "Campo Cidade é obrigatorio")
                .HasMaxLenght(x => x.State, 50, "Estado deve conter maximo 50 char")
                .HasMinLenght(x => x.State, 2, "Rua invalida")
                .IsRequired(x => x.State, "Estado não foi informado")
                .IsGreaterThan(x => x.Number, 0, "Numero deve ser maior que zero");
        }

        
    }
}
