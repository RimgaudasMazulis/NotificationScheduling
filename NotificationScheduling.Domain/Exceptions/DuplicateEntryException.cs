using System;

namespace NotificationScheduling.Domain.Exceptions
{
    public class DuplicateEntryException : Exception
    {
        public DuplicateEntryException()
        {
        }

        public DuplicateEntryException(string message) : base(message)
        {
        }
    }
}
