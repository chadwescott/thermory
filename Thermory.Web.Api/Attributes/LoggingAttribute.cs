using log4net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Thermory.Web.Api.Attributes
{
    //public class LoggingAttribute : ActionFilterAttribute
    //{
    //    private readonly ILog _log;
    //    public LoggingAttribute()
    //    {
    //        _log = LogManager.GetLogger(CC.Configuration.Constants.Logging.ApiLog);
    //    }

    //    public override void OnActionExecuting(HttpActionContext actionContext)
    //    {
    //        Log(actionContext, "API Call made.");
    //        base.OnActionExecuting(actionContext);
    //    }
    //    public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
    //    {
    //        Log(actionExecutedContext.ActionContext, "API Call completed");
    //        base.OnActionExecuted(actionExecutedContext);
    //    }

    //    private void Log(HttpActionContext actionContext, string logMsg)
    //    {
    //        var context = new ApiContext(actionContext.ModelState);

    //        var customerId = context.CustomerId;
    //        var projectId = context.ProjectId;
    //        var fullUrl = actionContext.Request.RequestUri.AbsoluteUri;
    //        var ip = AttributeHelpers.GetIpAddress(actionContext.Request);
    //        var controller = AttributeHelpers.GetController(actionContext.Request);
    //        var action = AttributeHelpers.GetAction(actionContext.Request);
    //        var accessKey = context.ApiKey;

    //        AttributeHelpers.SetLoggingValues(customerId, projectId, fullUrl, ip, controller, action, accessKey);
    //        AttributeHelpers.LogApiMessage(() => _log.Info(logMsg), actionContext.Request.RequestUri);
    //        AttributeHelpers.ClearLog4Net();
    //    }
    //}
}