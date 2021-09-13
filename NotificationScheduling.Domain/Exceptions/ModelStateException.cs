using System;

namespace NotificationScheduling.Domain.Exceptions
{
    public class ModelStateException : Exception
    {
        public ModelStateException()
        {
        }

        public ModelStateException(string message) : base(message)
        {
        }
    }
}
