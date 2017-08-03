using System;

namespace LF.SysAdm.Shared.Validations
{
    public class DomainNotification
    {
        protected DomainNotification() { }
        public DomainNotification( string value, string key)
        {
            Key = key;
            Value = value;
            RegistrationDate = DateTime.Now;
        }

        public string Key { get; private set; }
        public string Value { get; private set; }
        public DateTime RegistrationDate { get; private set; }
    }
}
