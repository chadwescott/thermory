using System;
using System.Web.Mvc;
using Thermory.Business;

namespace Thermory.Web.Controllers
{
    public class PackingSlipController : Controller
    {
        [Authorize]
        public ActionResult Index(Guid? id)
        {
            if (id == null)
                return RedirectToAction("Index", "SalesOrder");
            var package = CommandDirectory.Instance.GetPackageById(id.Value);
            return View(package);
        }
    }
}