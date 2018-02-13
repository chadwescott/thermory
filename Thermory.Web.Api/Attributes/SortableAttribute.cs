using System.Collections.Generic;
using System.Web;
using System.Web.Helpers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Thermory.Web.Api.Extensions;
using Thermory.Web.Api.Sorting;

namespace Thermory.Web.Api.Attributes
{
    public class SortableAttribute : ActionFilterAttribute
    {
        private readonly static Dictionary<SortType, SortConfig> ConfigLookup = new Dictionary<SortType, SortConfig>
        {
            { SortType.LumberCategory, LumberCategorySortConfig.Instance }
        };

        public readonly SortConfig Configuration;

        public SortableAttribute(SortType sortType)
        {
            Configuration = ConfigLookup[sortType];
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var context = new ApiContext(actionContext.ModelState);
            var queryString = actionContext.Request.RequestUri.Query;
            var queryParams = HttpUtility.ParseQueryString(queryString);

            var sortDirection = queryParams.ParseValue(Constants.Sorting.Direction, Constants.Sorting.Ascending);
            context.SortDirection = sortDirection == Constants.Sorting.Descending
                ? SortDirection.Descending
                : SortDirection.Ascending;
            var sortField = queryParams.ParseValue(Constants.Sorting.Field, string.Empty);
            context.SortField = Configuration.Fields.Contains(sortField) ? sortField : Configuration.DefaultSortField;
        }
    }
}