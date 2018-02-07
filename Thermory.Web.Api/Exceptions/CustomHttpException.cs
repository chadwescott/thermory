using System;
using System.Net;

namespace Thermory.Web.Api.Exceptions
{
    public class CustomHttpException : Exception
    {
        public readonly HttpStatusCode StatusCode;

        public CustomHttpException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}