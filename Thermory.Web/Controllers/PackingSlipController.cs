using System;
using System.Linq;
using System.Web.Mvc;
using Thermory.Business;
using Thermory.Web.Models;

namespace Thermory.Web.Controllers
{
    public class PackingSlipController : Controller
    {
        [Attributes.Authorize]
        public ActionResult Index(Guid? orderId, int? packageNumber)
        {
            if (orderId == null || packageNumber == null)
                return RedirectToAction("Index", "SalesOrder");

            var order = CommandDirectory.Instance.GetOrderById(orderId.Value);
            if (order == null)
                return RedirectToAction("Index", "SalesOrder");

            var package = order.Packages.SingleOrDefault(p => p.PackageNumber == packageNumber);
            if (package == null)
                return RedirectToAction("Index", "SalesOrder");

            return View(new PackingSlip{ Package = package, TotalPackages = order.Packages.Count});
        }
    }
}