using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Thermory.Web.Api.Constants;
using Thermory.Web.Api.Extensions;

namespace Thermory.Web.Api.Attributes
{
    public class SupportsPagingAttribute : ActionFilterAttribute
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = Pagination.DefaultPageSize;

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var context = new ApiContext(actionContext.ModelState);
            var queryString = actionContext.Request.RequestUri.Query;
            var queryParams = HttpUtility.ParseQueryString(queryString);

            context.PageNumber = PageNumber = queryParams.ParseValue(Pagination.PageNumber, 1);
            context.PageSize = PageSize = queryParams.ParseValue(Pagination.PageSize, Pagination.DefaultPageSize);
        }
    }
}