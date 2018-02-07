using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;
using Thermory.Web.Api.Exceptions;

namespace Thermory.Web.Api.Attributes
{
    public class ApiExceptionAttribute : ExceptionFilterAttribute
    {
        private readonly HashSet<HttpStatusCode> _badHttpStatusCodes = new HashSet<HttpStatusCode>()
        {
            HttpStatusCode.InternalServerError,
            HttpStatusCode.BadRequest,
            HttpStatusCode.BadGateway,
            HttpStatusCode.Gone
        };

        private bool _isInitialized;

        public ApiExceptionAttribute()
        {
        }

        private void Initialize()
        {
            _isInitialized = true;
        }

        public override void OnException(HttpActionExecutedContext context)
        {
            if (!_isInitialized)
                Initialize();

            var modelState = new ModelStateDictionary();
            var statusCode = HttpStatusCode.InternalServerError;
            string description = null;

            if (context.Exception is CustomHttpException)
            {
                var e = (CustomHttpException)context.Exception;
                statusCode = e.StatusCode;
                description = e.Message;
            }
            else
            {
                if (context.Exception is HttpResponseException)
                {
                    var e = (HttpResponseException)context.Exception;
                    statusCode = e.Response.StatusCode;
                }
            }

            modelState.AddModelError("ErrorCode", statusCode.ToString());

            if (!String.IsNullOrWhiteSpace(description))
                modelState.AddModelError("ErrorMessage", description);

            if (!_badHttpStatusCodes.Contains(statusCode))
            {
                context.Response = context.Request.CreateResponse<ErrorModel>(statusCode, new ErrorModel() { ErrorCode = statusCode.ToString(), ErrorMessage = description });
            }
            else
            {
                context.Response = context.Request.CreateErrorResponse(statusCode, modelState);
            }
        }
    }

    public class ErrorModel
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public ErrorModel() { }
    }
}