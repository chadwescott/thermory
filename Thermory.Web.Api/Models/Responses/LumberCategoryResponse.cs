using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Thermory.Domain.Models;
using Thermory.Web.Api.Constants;
using Thermory.Web.Api.ExtensionMethods;

namespace Thermory.Web.Api.Models.Responses
{
    [JsonObject(Constants.Responses.LumberCategoryResponse)]
    public class LumberCategoryResponse : IApiResponse
    {
        [JsonProperty(Routes.LumberCategories)]
        public IList<LumberCategoryResponseModel> LumberCategories { get; set; }

        private LumberCategoryResponse()
        {
            LumberCategories = new List<LumberCategoryResponseModel>();
        }

        public static HttpResponseMessage CreateResponse(HttpRequestMessage request, IEnumerable<LumberCategory> lumberCategories)
        {
            var response = new LumberCategoryResponse();
            response.LumberCategories = lumberCategories.Select(x => LumberCategoryResponseModel.ToResponseModel(x)).ToList();
            return response.HttpResponse(request);
        }
    }

    public class LumberCategoryResponseModel
    {
        public static LumberCategoryResponseModel ToResponseModel(LumberCategory lumberCategory)
        {
            return new LumberCategoryResponseModel();
        }
    }
}