using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Querys.User;
using System;

namespace LF.SysAdm.Domain.Repositorys
{
    public interface IRepositoryUser: ICRUD<Users>
    {
        Users Authenticate(string email, string password);
        bool IsUserExist(string email);
        Users GetUserEmail(string email);
        UserQuery GetUser(Guid Id);
    }
}
