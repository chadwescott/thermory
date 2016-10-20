using System;
using System.Web;

namespace Thermory.Web
{
    public static class HttprequestBaseExtensions
    {
        public static string DomainUrl(this HttpRequestBase request)
        {
            if (request == null || request.Url == null)
                return string.Empty;
            return string.Format("{0}{1}{2}{3}", request.Url.Scheme, Uri.SchemeDelimiter, request.Url.Host,
                request.Url.IsDefaultPort ? "" : ":" + request.Url.Port);
        }
    }
}