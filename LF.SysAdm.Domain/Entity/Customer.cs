using LF.SysAdm.Domain.Entity.Base;
using LF.SysAdm.Shared.Validations;
using System;
using System.Collections.Generic;

namespace LF.SysAdm.Domain.Entity
{
    public class Customer : BaseEntity
    {
        protected Customer() { }

        public Customer(string doc, DateTime dtBirtday, string phone, bool gender, Users user)
        {
            this.Document = doc;
            this.DateBirthday = dtBirtday;
            this.Phone = phone;
            this.Gender = gender;
            this.Rel_User = user;
            this.Rel_AdressList = new List<Address>();
            this.DateRegister = DateTime.Now;
            this.Rel_User = user;
            this.UserId = user.ID;
            Register();
        }


        public string Document { get; private set; }
        public DateTime DateBirthday { get; private set; }
        public string Phone { get; private set; }
        public bool Gender { get; private set; }
        public DateTime DateRegister { get; private set; }
        public DateTime? DateOfChange { get; private set; }

        public Guid UserId { get; private set; }
        public virtual Users Rel_User { get; private set; }

        public ICollection<Address> Rel_AdressList { get; private set; }

        public override void Register()
        {
                 new ValidationContract<Customer>(this)
                .HasMaxLenght(x => x.Document, 14, "Tamanho maximo do CPF 14 com . e -")
                .HasMinLenght(x => x.Document, 14, "Tamanho minimo do CPF 14 com . e -")
                .IsRequired(x => x.Document, "Documento não foi informado")               
                .IsLowerThan(x => x.DateBirthday, DateTime.Now, "Data de Aniversario deve ser inferior")
                .IsGreaterThan(x => x.DateBirthday, new DateTime(1970, 01, 01), "Ano de Nascimento deve ser maio que 1970")     
                .HasMaxLenght(x => x.Phone, 14, "Numero de Telfone deve conter no maximo 14 char")
                .HasMinLenght(x => x.Phone, 13, "Numero de Telfone deve conter no minimo 13 char")
                .IsRequired(x => x.Phone, "Telefone não foi informado")                
                .IsNotNull(Rel_User, "Usuario deve estar cadastrado no banco"); 
        }

        public void Edit(string doc, DateTime dtBirtday, string phone, bool gender)
        {
            this.Document = doc;
            this.DateBirthday = dtBirtday;
            this.Phone = phone;
            this.Gender = gender;
            this.DateOfChange = DateTime.Now;

            var validation = new ValidationContract<Customer>(this)
               .HasMaxLenght(x => x.Document, 14, "Tamanho maximo do CPF 14 com . e -")
               .HasMinLenght(x => x.Document, 14, "Tamanho minimo do CPF 14 com . e -")
               .IsRequired(x => x.Document, "Documento é Obrigatorio")
               .IsLowerThan(x => x.DateBirthday, DateTime.Now, "Data de Aniversario deve ser inferior")
               .IsGreaterThan(x => x.DateBirthday, new DateTime(1970, 01, 01), "Ano de Nascimento deve ser maio que 1970")
               .HasMaxLenght(x => x.Phone, 14, "Numero de Telfone deve conter no maximo 14 char")
               .HasMinLenght(x => x.Phone, 13, "Numero de Telfone deve conter no minimo 14 char")
               .IsRequired(x => x.Phone, "Telefone não foi informado");
        }

      
    }
}
