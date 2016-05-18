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

        protected override OrderStatuses InitialOrderStatus
        {
            get { return OrderStatuses.SentToWarehouse; }
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}