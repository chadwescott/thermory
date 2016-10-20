using System.Web.Mvc;
using Thermory.Web.Attributes;
using AuthorizeAttribute = Thermory.Web.Attributes.AuthorizeAttribute;

namespace Thermory.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomHandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
        }
    }
}