using System.Web.Http;
using Thermory.Web.Api.Attributes;

namespace Thermory.Web.Api.Controllers
{
    [ApiException]
    [ValidateModel]
    //[SwaggerResponse(HttpStatusCode.Unauthorized, Constants.ApiErrorMessages.NoValidApiKey)]
    public abstract class BaseApiController : ApiController
    {
        protected ApiContext Context;

        protected BaseApiController()
            : base()
        {
            Context = new ApiContext(ModelState);
        }
    }
}