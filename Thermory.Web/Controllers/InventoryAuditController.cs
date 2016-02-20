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
                LumberCategories = CommandDirectory.Instance.GetAllLumberCategories(),
                MiscellaneousCategories = CommandDirectory.Instance.GetAllMiscellaneousCategories()
            };
            return View(model);
        }

        [HttpPost]
        public JsonResult Save(LumberProduct[] lumberProducts, MiscellaneousProduct[] miscProducts)
        {
            var lumberInventory = lumberProducts ?? new ILumberProduct[0];
            var miscInventory = miscProducts ?? new IMiscellaneousProduct[0];
            CommandDirectory.Instance.UpdateProductInventory(lumberInventory, miscInventory);
            return Json(new { status = "success"});
        }
    }
}
