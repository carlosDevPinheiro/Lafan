using System.Collections.Generic;

namespace LF.SysAdm.Shared.Validations
{
    public abstract class Notifiable
    {
        private List<DomainNotification> _errors;

        // costrutor
        public Notifiable()
        {
            _errors = new List<DomainNotification>();
        }

        public void AddNotification(string value, string key)
        {
            _errors.Add(new DomainNotification(key, value));
        }

        public List<DomainNotification> ListErrors()
        {
           return _errors;
        }

        public bool IsNotification()
        {
            return _errors.Count.Equals(0) ? true : false;
        }

        

        public void Dispose()
        {
            _errors = new List<DomainNotification>();
        }
    }
}
