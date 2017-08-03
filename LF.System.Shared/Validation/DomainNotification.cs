using System;

namespace LF.System.Shared.Validation
{
    public class DomainNotification
    {
        protected DomainNotification() { }
        public DomainNotification(string key, string value)
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
