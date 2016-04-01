using System.Web.Mvc;

namespace Thermory.Web.Controllers
{
    public class PageNotFoundController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}