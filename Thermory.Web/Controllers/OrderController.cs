using System.Web.Mvc;
using Thermory.Web.Models;

namespace Thermory.Web.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /SalesOrder/

        public ActionResult Sample()
        {
            return View(new SalesOrderViewModel());
        }

    }
}
