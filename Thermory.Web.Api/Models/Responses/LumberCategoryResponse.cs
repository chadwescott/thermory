using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Thermory.Domain.Models;
using Thermory.Web.Api.Constants;
using Thermory.Web.Api.Extensions;
using Thermory.Web.Api.Routing;

namespace Thermory.Web.Api.Models.Responses
{
    [JsonObject(Constants.Responses.LumberCategoryResponse)]
    public class LumberCategoryResponse : IApiResponse
    {
        [JsonProperty(Routes.LumberCategories)]
        public IList<LumberCategoryResponseModel> LumberCategories { get; set; }

        private LumberCategoryResponse(IEnumerable<LumberCategory> lumberCategories)
        {
            LumberCategories = lumberCategories.Select(c => new LumberCategoryResponseModel(c)).ToList();
        }

        public static HttpResponseMessage CreateResponse(HttpRequestMessage request, IEnumerable<LumberCategory> lumberCategories)
        {
            return new LumberCategoryResponse(lumberCategories).HttpResponse(request);
        }
    }

    public class LumberCategoryResponseModel : LinkedResource
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public LumberCategoryResponseModel(LumberCategory category)
        {
            Id = category.Id;
            Name = category.Name;
            Links.Add(Link.MakeLink(LumberCategoryRoute.GetLumberCategorysById(Versions.V1, category), HttpMethod.Get, LinkType.Self));
        }
    }
}