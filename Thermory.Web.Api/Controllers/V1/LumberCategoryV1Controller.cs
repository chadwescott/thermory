using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using Thermory.Domain.Models;
using Thermory.Web.Api.Models.Responses;

namespace Thermory.Web.Api.Controllers.V1
{
    public class LumberCategoryV1Controller : ApiController
    {
        [HttpGet]
        [Route("", Name = nameof(GetLumberCategories))]
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
