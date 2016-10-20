using System.Net.Mime;
using System.Web.Mvc;

namespace Thermory.Web.Attributes
{
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        private static readonly log4net.ILog Logger =
               log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public override void OnException(ExceptionContext exceptionContext)
        {
            var exception = exceptionContext.Exception;
            exceptionContext.Result = new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            Logger.Fatal(exception);

            if (exceptionContext.HttpContext.Request.IsAjaxRequest())
            {
                var innermostException = exception;
                while (innermostException.InnerException != null)
                    innermostException = innermostException.InnerException;

                var errorMessage = innermostException.Message;
                var response = exceptionContext.RequestContext.HttpContext.Response;
                response.TrySkipIisCustomErrors = true;
                response.Write(errorMessage);
                response.ContentType = MediaTypeNames.Text.Plain;
                exceptionContext.ExceptionHandled = true;
            }
            base.OnException(exceptionContext);
        }

    }
}

