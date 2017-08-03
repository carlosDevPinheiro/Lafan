using System;

namespace LF.SysAdm.Domain.Command.User
{
    public class EditUserCommand
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
       
    }
}
