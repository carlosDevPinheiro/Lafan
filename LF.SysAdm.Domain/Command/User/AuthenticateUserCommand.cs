using LF.SysAdm.Shared.Utils;

namespace LF.SysAdm.Domain.Command.User
{
    public class AuthenticateUserCommand
    {
        public string Email { get; set; }
        

        public string Password { get; set; }
        

    }
}
