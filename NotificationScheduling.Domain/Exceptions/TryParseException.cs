using System;

namespace NotificationScheduling.Domain.Exceptions
{
    public class TryParseException : Exception
    {
        public TryParseException()
        {
        }
        public TryParseException(string message) : base(message)
        {
        }
    }
}
