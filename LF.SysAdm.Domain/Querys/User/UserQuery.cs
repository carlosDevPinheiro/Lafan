using System;

namespace LF.SysAdm.Domain.Querys.User
{
    public class UserQuery : BaseQuery
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? DateofChange { get; set; }
        public String Profile { get; set; }
        public bool Active { get; set; }
    }
}
