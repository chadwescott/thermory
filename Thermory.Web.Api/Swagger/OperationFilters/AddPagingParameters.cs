using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Description;
using Thermory.Web.Api.Attributes;
using Thermory.Web.Api.Constants;

namespace Thermory.Web.Api.Swagger.OperationFilters
{
    /// <summary>
    /// This operation filter will check if a method is decorated with the
    /// SuppportsPaging attribute and if it is, will add the parameters to
    /// the Swagger documentation. This enforces consistency between all
    /// methods that support paging.
    /// </summary>
    public class AddPagingParameters : IOperationFilter
    {
        private static Parameter PageNumberParam =
            new Parameter
            {
                name = Pagination.PageNumber,
                required = false,
                description = "The page number",
                @default = 1,
                type = "integer",
                @in = "query"
            };

        private static Parameter PageSizeParam =
            new Parameter
            {
                name = Pagination.PageSize,
                required = false,
                description = "The page size",
                @default = Pagination.DefaultPageSize,
                type = "integer",
                @in = "query"
            };

        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var attr = apiDescription.GetControllerAndActionAttributes<SupportsPagingAttribute>().FirstOrDefault();
            if (attr == null)
                return;

            if (operation.parameters == null)
                operation.parameters = new List<Parameter>();

            operation.parameters.Add(PageNumberParam);
            operation.parameters.Add(PageSizeParam);
        }
    }
}