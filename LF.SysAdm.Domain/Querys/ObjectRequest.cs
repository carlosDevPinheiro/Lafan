using LF.SysAdm.Shared.Validations;
using System.Collections.Generic;

namespace LF.SysAdm.Domain.Querys
{
    public class ObjectRequest
    {
        public object Data { get; private set; }
        public bool Success { get; private set; }
        public object Errors { get; private set; }
        public ObjectRequest CreateObjectRequest(object response, bool success)
        {
            if (success)
            {
                Success = success;
                Data = response;
            }

            Success = success;
            Errors = response;

            return this;
        }

        public ObjectRequest CreateErrorNotification(IEnumerable<DomainNotification> notifications)
        {
            Success = false;
            Errors = notifications;
            return this;
        }
    }
}
