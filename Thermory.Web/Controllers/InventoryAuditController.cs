using System.Web.Mvc;
using Thermory.Business;
using Thermory.Domain;
using Thermory.Web.Models;

namespace Thermory.Web.Controllers
{
    public class InventoryAuditController : Controller
    {
        public ActionResult Index()
        {
            var model = new InventoryWorksheet
            {
                LumberCategories = CommandDirectory.Instance.GetAllLumberCategories()
            };
            return View(model);
        }

        [HttpPost]
        public JsonResult Index(LumberProduct[] lumberProducts)
        {
            CommandDirectory.Instance.UpdateLumberProductInventory(lumberProducts);
            return Json(new { status = "success"});
        }
    }
}
