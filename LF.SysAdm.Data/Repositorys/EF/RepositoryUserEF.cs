using LF.SysAdm.Data.Context;
using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Repositorys;
using System.Linq;
using LF.SysAdm.Domain.Querys.User;
using System;

namespace LF.SysAdm.Data.Repositorys.EF
{
    public class RepositoryUserEF : CRUDEF<Users>, IRepositoryUser
    {
        DbContextEF _context;
        public RepositoryUserEF(IDbConnectionContext context) : base(context)
        {
            _context = (DbContextEF)context;
        }

        public Users Authenticate(string email, string password)
        {
            return _context.Set<Users>().FirstOrDefault(x => x.Email.Equals(email) && x.Password.Equals(password));
        }

        public UserQuery GetUser(Guid Id)
        {
            return _context.Set<Users>().Select(x => new UserQuery
            {
                ID = x.ID,
                Active = x.Active,
                DateofChange = x.DateofChange,
                Email = x.Email,
                Name = x.Name,
                Profile = x.Profile.ToString(),
                RegistrationDate = x.RegistrationDate

            }).FirstOrDefault(x => x.ID == Id);
        }

        public Users GetUserEmail(string email) => _context.Set<Users>().FirstOrDefault(x => x.Email.Equals(email));

        public bool IsUserExist(string email) => _context.Set<Users>().Any(x => x.Email.Equals(email));

    }
}
