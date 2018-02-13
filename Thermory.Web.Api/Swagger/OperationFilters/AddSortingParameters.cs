using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Description;
using Thermory.Web.Api.Attributes;
using Thermory.Web.Api.Sorting;

namespace Thermory.Web.Api.Swagger.OperationFilters
{
    public class AddSortingParameters : IOperationFilter
    {
        private static Parameter SortDirectionParam =
            new Parameter
            {
                @default = Constants.Sorting.Ascending,
                description = "The sort direction",
                @enum = new List<object> { Constants.Sorting.Ascending, Constants.Sorting.Descending },
                @in = "query",
                name = Constants.Sorting.Direction,
                required = false,
                type = "string"
            };

        private static Parameter GetSortFieldParam(ISortConfig config)
        {
            return new Parameter
            {
                @default = config.DefaultSortField,
                description = "The sort fields",
                @enum = config.Fields.ToList<object>(),
                @in = "query",
                name = Constants.Sorting.Field,
                required = false,
                type = "string"
            };
        }

        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var attr = apiDescription.GetControllerAndActionAttributes<SortableAttribute>().FirstOrDefault();
            if (attr == null)
                return;

            if (operation.parameters == null)
                operation.parameters = new List<Parameter>();

            operation.parameters.Add(SortDirectionParam);
            operation.parameters.Add(GetSortFieldParam(attr.Configuration));
        }
    }
}