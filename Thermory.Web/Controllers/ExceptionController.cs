using System.Web.Mvc;
using Thermory.Web.Models;

namespace Thermory.Web.Controllers
{
    [HandleError]
    public class ExceptionController : Controller
    {
        public ActionResult Error()
        {
            var errorInfo = new ErrorInfo
            {
                Message = "An Error Has Occured",
                Description = "An unexpected error occured on our website."
            };
            return View("Exception", errorInfo);
        }

        public ActionResult BadRequest()
        {
            var errorInfo = new ErrorInfo
            {
                Message = "Bad Request",
                Description = "The request cannot be fulfilled due to bad syntax."
            };
            return View("Exception", errorInfo);
        }

        public ActionResult NotFound()
        {
            var errorInfo = new ErrorInfo
            {
                Message = "We are sorry, the page you requested cannot be found.",
                Description = "The URL may be misspelled or the page you're looking for is no longer available."
            };
            return View("Exception", errorInfo);
        }

        public ActionResult Forbidden()
        {
            var errorInfo = new ErrorInfo
            {
                Message = "403 Forbidden",
                Description = "Forbidden: You don't have permission to access the requested page."
            };
            return View("Exception", errorInfo);
        }

        public ActionResult UrlTooLong()
        {
            var errorInfo = new ErrorInfo
            {
                Message = "URL Too Long",
                Description = "The requested URL is too large to process. That’s all we know."
            };
            return View("Exception", errorInfo);
        }

        public ActionResult ServiceUnavailable()
        {
            var errorInfo = new ErrorInfo
            {
                Message = "Service Unavailable",
                Description =
                    "Our apologies for the temporary inconvenience. This is due to overloading or maintenance of the server."
            };
            return View("Exception", errorInfo);
        }
    }
}