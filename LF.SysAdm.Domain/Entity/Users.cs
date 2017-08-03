using LF.SysAdm.Domain.Entity.Base;
using LF.SysAdm.Domain.Enum;
using LF.SysAdm.Shared.Utils;
using LF.SysAdm.Shared.Validations;
using System;

namespace LF.SysAdm.Domain.Entity
{
    public class Users : BaseEntity
    {
        protected Users()
        {

        }
        public Users(string name, string email, string password)
        {
          
            Name = Helpers.Capitalize(name);
            Password = password;
            Email = email.ToLower();
            RegistrationDate = DateTime.Now;
            Active = true;
            Profile = EProfile.Commom;
            Register();
        }

      
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public DateTime? DateofChange { get; private set; }
        public EProfile Profile { get; private set; }
        public bool Active { get; private set; }

        public override void Register()
        {
                new ValidationContract<Users>(this)
                .HasMaxLenght(x => x.Name, 50, "Nome deve conter até 50 char")
                .HasMinLenght(x => x.Name, 5, "Nome deve ter no minimo 5 char")
                .IsRequired(x => x.Name, "Nome do Usuario não foi informado")
                .IsEmail(x => x.Email, "Email invalido")
                .HasMaxLenght(x => x.Password, 32)
                .HasMinLenght(x => x.Password, 6)
                .IsRequired(x => x.Password, "Senha  do Usuario nao informada");

            Password = SecurityPassword.Encrypt(Password);
        }

        public void Update(string name, string email, string password)
        {
            Name = Helpers.Capitalize(name);
            Email = email.ToLower();
            Password = password;
           

            new ValidationContract<Users>(this)
                .HasMaxLenght(x => x.Name, 50, "Nome deve conter até 50 char")
                .HasMinLenght(x => x.Name, 5, "Nome deve ter no minimo 5 char")
                .IsRequired(x => x.Name, "Nome do Usuario não foi informado")
                .IsEmail(x => x.Email, "Email invalido")
                .HasMaxLenght(x => x.Password, 32)
                .HasMinLenght(x => x.Password, 6)
                .IsRequired(x => x.Password, "Senha  do Usuario nao informada");

            Password = SecurityPassword.Encrypt(Password);
            DateofChange = DateTime.Now;
        }

        public void UpDateProfile(EProfile profile)
        {
            Profile = profile;
            DateofChange = DateTime.Now;
        }

        public string GetProfile(EProfile profile)
        {
            return profile.ToString();
        }
    }
}
