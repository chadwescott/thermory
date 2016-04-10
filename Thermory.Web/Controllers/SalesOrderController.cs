using System.Web.Mvc;
using Thermory.Domain.Enums;

namespace Thermory.Web.Controllers
{
    public class SalesOrderController : OrderController
    {
        protected override OrderTypes OrderType
        {
            get { return OrderTypes.SalesOrder; }
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}