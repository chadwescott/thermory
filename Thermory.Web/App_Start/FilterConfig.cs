using System.Web.Mvc;

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