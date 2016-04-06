using System;
using System.Web;

namespace Thermory.Web
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                throw new HttpException(403, "Forbidden");
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}