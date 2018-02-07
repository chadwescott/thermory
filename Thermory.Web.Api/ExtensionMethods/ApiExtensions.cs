using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Thermory.Web.Api.Models.Responses;

namespace Thermory.Web.Api.ExtensionMethods
{
    public static class ApiExtensions
    {
        public static IEnumerable<T> ToEnumerable<T>(this T item)
        {
            yield return item;
        }

        public static HttpResponseMessage HttpResponse<T>(this T content, HttpRequestMessage request, HttpStatusCode code = HttpStatusCode.OK, string location = null)
            where T : IApiResponse
        {
            var response = request.CreateResponse(code, content);
            if (location != null)
                response.Headers.Location = new Uri(location);
            return response;
        }
    }
}