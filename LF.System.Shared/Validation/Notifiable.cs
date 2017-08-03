using System.Collections.Generic;

namespace LF.System.Shared.Validation
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

        public IReadOnlyCollection<DomainNotification> ListErrors()
        {
            IReadOnlyCollection<DomainNotification> erros = _errors;

            return erros;
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
