using System;
using System.Linq;
using System.Web.Mvc;
using Thermory.Business;
using Thermory.Web.Models;

namespace Thermory.Web.Controllers
{
    public class ProductFamilyController : Controller
    {
        public ActionResult Index(Guid? id)
        {
            var rootProductFamilies = CommandDirectory.Instance.GetRootProductFamilies();
            var activeProductFamily = rootProductFamilies.SingleOrDefault(pf => pf.Id == id);
            var model = new ProductFamilyIndex
            {
                ActiveProductFamily = activeProductFamily,
                RootProductFamilies = CommandDirectory.Instance.GetRootProductFamilies()
            };
            return View(model);
        }

    }
}
