using Swashbuckle.Swagger;
using System.Linq;
using System.Web.Http.Description;
using Thermory.Web.Api.Attributes;

namespace Thermory.Web.Api.Swagger.OperationFilters
{
    public class AddImplementationNotes : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var attr = apiDescription.GetControllerAndActionAttributes<SwaggerImplementationNotesAttribute>().FirstOrDefault();
            if (attr != null)
                operation.description = attr.ImplementationNotes;
        }
    }
}