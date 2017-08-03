using LF.SysAdm.Domain.Command.User;
using LF.SysAdm.Domain.Entity;
using LF.SysAdm.Domain.Querys;
using System;

namespace LF.SysAdm.Domain.Business
{
    public interface IBusinessUser
    {
        ObjectRequest NewUser(RegisterUsersCommand cmd);
        ObjectRequest GetUser(Guid Id);
        ObjectRequest UpdateUser(EditUserCommand cmd);
        ObjectRequest GetUsers();
        ObjectRequest Remove(Guid id);
        Users GetUserOAuth(string email, string password);
    }
}
