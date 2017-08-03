using LF.System.Domain.Entity.Base;
using LF.System.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF.System.Domain.Entity
{
    public class Users// : BaseEntity
    {
        protected Users()
        {

        }
        //public Users(string name, string email, string password)
        //{
        //    UserId = Guid.NewGuid();
        //    Name = Helpers.Capitalize(name);
        //    Password = password;
        //    Email = email;
        //    RegistrationDate = DateTime.Now;
        //    Active = true;
        //    Profile = EProfile.Commom;
        //    Register();
        //}

        //public Guid UserId { get; private set; }
        //public string Name { get; private set; }
        //public string Email { get; private set; }
        //public string Password { get; private set; }
        //public DateTime RegistrationDate { get; private set; }
        //public DateTime? DateofChange { get; private set; }
        //public EProfile Profile { get; private set; }
        //public bool Active { get; private set; }

        //public override void Register()
        //{
        //    var validUser = new ValidationContract<Users>(this)
        //        .HasMaxLenght(x => x.Name, 50)
        //        .HasMinLenght(x => x.Name, 5)
        //        .HasMaxLenght(x => x.Email, 100)
        //        .HasMinLenght(x => x.Email, 5)
        //        .IsEmail(x => x.Email)
        //        .HasMaxLenght(x => x.Password, 32)
        //        .HasMinLenght(x => x.Password, 6);

        //    Password = SecurityPassword.Encrypt(Password);
        //}

        //public void Update(string name, string email, string password)
        //{
        //    Name = name;
        //    Email = email;
        //    Password = password;
        //    DateofChange = DateTime.Now;

        //    var validUser = new ValidationContract<Users>(this)
        //        .HasMaxLenght(x => x.Name, 50)
        //        .HasMinLenght(x => x.Name, 5)
        //        .HasMaxLenght(x => x.Email, 100)
        //        .HasMinLenght(x => x.Email, 5)
        //        .IsEmail(x => x.Email)
        //        .HasMaxLenght(x => x.Password, 32)
        //        .HasMinLenght(x => x.Password, 6);

        //    Password = SecurityPassword.Encrypt(Password);
        //}

        //public void UpDateProfile(EProfile profile)
        //{
        //    Profile = profile;
        //}

        
    }
}
