using System.Web.Mvc;
using Thermory.Business;
using Thermory.Web.Models;

namespace Thermory.Web.Controllers
{
    public class InventoryWorksheetController : Controller
    {
        public ActionResult Index()
        {
            var model = new InventoryWorksheet
            {
                LumberCategories = CommandDirectory.Instance.GetAllLumberProductsWithInventory(),
                //MiscellaneousCategories = CommandDirectory.Instance.GetAllMiscellaneousProductsWithInventory()
            };
            return View(model);
        }
    }
}
