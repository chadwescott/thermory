using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Thermory.Domain.Models;
using Thermory.Web.Api.Attributes;
using Thermory.Web.Api.Constants;
using Thermory.Web.Api.Models.Responses;
using Thermory.Web.Api.Sorting;

namespace Thermory.Web.Api.Controllers.V1
{
    [RoutePrefix(Routes.Api + "/" + Versions.Names.V1 + "/" + Routes.LumberCategories)]
    public class LumberCategoryV1Controller : BaseApiController
    {
        [HttpGet]
        [Route("", Name = nameof(GetLumberCategories))]
        [SwaggerOperation(OperationId = "getLumberCategories", Tags = new[] { "Tag 1" })]
        [SwaggerImplementationNotes("Returns the lumber categories.")]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(LumberCategoryResponse))]
        [SupportsPaging]
        [Sortable(SortType.LumberCategory)]
        public HttpResponseMessage GetLumberCategories()
        {
            var categories = new List<LumberCategory> {
                new LumberCategory
                {
                    Id = Guid.NewGuid(),
                    Name = "Category 1"
                }
            };

            return LumberCategoryResponse.CreateResponse(Request, categories);
        }
    }
}
