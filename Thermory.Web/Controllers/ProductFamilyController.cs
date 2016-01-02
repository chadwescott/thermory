using System.Web.Mvc;
using Thermory.Business;
using Thermory.Web.Models;

namespace Thermory.Web.Controllers
{
    public class ProductFamilyController : Controller
    {
        public ActionResult Index()
        {
            var model = new ProductFamilyIndex
            {
                RootProductFamilies = CommandDirectory.Instance.GetRootProductFamilies()
            };
            return View(model);
        }

    }
}
