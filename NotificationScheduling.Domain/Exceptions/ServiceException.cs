using System;
using System.Net;

namespace NotificationScheduling.Domain.Exceptions
{
    public class ServiceException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public string ErrorCode { get; }

        public ServiceException(string errorCode, string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }

        public ServiceException(string errorCode) : this(errorCode, string.Empty, HttpStatusCode.BadRequest) { }

        public ServiceException(string errorCode, string message) : this(errorCode, message, HttpStatusCode.BadRequest) { }

        public ServiceException(string errorCode, HttpStatusCode statusCode) : this(errorCode, string.Empty, statusCode) { }
    }
}
