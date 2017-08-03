using LF.SysAdm.Domain.Entity.Base;
using LF.SysAdm.Shared.Validations;
using System;

namespace LF.SysAdm.Domain.Entity
{
    public class Supply : BaseEntity
    {
        protected Supply() { }
        public Supply(string name, string cnpj, string phone, string agent, string email, Address address)
        {
            CompanyName = name;
            CNPJ = cnpj;
            Phone = phone;
            Agent = agent;
            Email = email;
            DateRegister = DateTime.Now;
            Rel_Address = address;
            AddressId = address.ID;
            Register();
        }

        public string CompanyName { get; private set; }
        public string CNPJ { get; private set; }
        public string Phone { get; private set; }
        public string Agent { get; private set; }
        public string Email { get; private set; }
        public DateTime DateRegister { get; set; }
        public DateTime? DateOfChange { get; private set; }

        public Guid AddressId { get; private set; }
        public Address Rel_Address { get; private set; }

        public override void Register()
        {
                   new ValidationContract<Supply>(this)
                  .HasMaxLenght(x => x.CompanyName, 50, "O Nome da Compania deve ter no maximo 50 char")
                  .HasMinLenght(x => x.CompanyName, 4, "O Nome da Compania deve ter no minimo 4 char")
                  .IsRequired(x => x.CompanyName, "Nome do Fornecedor não foi informado")
                  .IsFixedLenght(x => x.CNPJ, 18, "O CNPJ esta incorreto")
                  .IsRequired(x => x.CNPJ, "CNPJ não foi informado")
                  .HasMaxLenght(x => x.Phone, 14, "O Numero de Telfone deve conter ate 14 char")
                  .HasMinLenght(x => x.Phone, 13, "O Numero de telefone deve ter no minimo 13 char")
                  .IsRequired(x => x.Phone, "Telefone não foi informado")
                  .HasMaxLenght(x => x.Agent, 40, "O Tamnho maximo para o Contato é de 40 char")
                  .HasMinLenght(x => x.Agent, 5, "O Tamnho minimo para o Contato é de 5 char")
                  .IsRequired(x => x.Agent, "Contato não foi informado")
                  .IsEmail(x => x.Email, "Digite um email valido");
        }

        public void Edite(string companyName, string cnpj, string phone, string agent, string email)
        {
            CompanyName = companyName;
            CNPJ = cnpj;
            Phone = phone;
            Agent = agent;
            Email = email;

                   new ValidationContract<Supply>(this)
                  .HasMaxLenght(x => x.CompanyName, 50, "O Nome da Compania deve ter no maximo 50 char")
                  .HasMinLenght(x => x.CompanyName, 4, "O Nome da Compania deve ter no minimo 4 char")
                  .IsRequired(x => x.CompanyName, "Nome do Fornecedor não foi informado")
                  .IsFixedLenght(x => x.CNPJ, 18, "O CNPJ esta incorreto")
                  .IsRequired(x => x.CNPJ, "CNPJ não foi informado")
                  .HasMaxLenght(x => x.Phone, 14, "O Numero de Telfone deve conter ate 14 char")
                  .HasMinLenght(x => x.Phone, 13, "O Numero de telefone deve ter no minimo 13 char")
                  .IsRequired(x => x.Phone, "Telefone não foi informado")
                  .HasMaxLenght(x => x.Agent, 40, "O Tamnho maximo para o Contato é de 40 char")
                  .HasMinLenght(x => x.Agent, 5, "O Tamnho minimo para o Contato é de 5 char")
                  .IsRequired(x => x.Agent, "Contato não foi informado")
                  .IsEmail(x => x.Email, "Digite um email valido");

            DateOfChange = DateTime.Now;
        }
    }
}
