using System.Web.Mvc;
using Thermory.Business;
using Thermory.Web.ViewModels;

namespace Thermory.Web.Controllers
{
    public class InventoryWorksheetController : Controller
    {
        public ActionResult Index()
        {
            var model = new InventoryViewModel
            {
                LumberCategories = CommandDirectory.Instance.GetAllLumberCategories(),
                MiscellaneousCategories = CommandDirectory.Instance.GetAllMiscellaneousCategories()
            };
            return View(model);
        }
    }
}
