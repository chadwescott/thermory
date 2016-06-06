using System.Web.Mvc;
using Thermory.Business;
using Thermory.Domain.Constants;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;
using WebMatrix.WebData;

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

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = Role.WarehouseCrew)]
        [HttpPost]
        public ActionResult WarehouseReceived(Order order)
        {
            CommandDirectory.Instance.WarehouseReceivedOrder(WebSecurity.CurrentUserId, order);
            return Json(new { status = "success" });
        }

        [Authorize(Roles = Role.WarehouseCrew)]
        [HttpPost]
        public ActionResult Pulled(Order order)
        {
            CommandDirectory.Instance.PullOrder(WebSecurity.CurrentUserId, order);
            return Json(new { status = "success" });
        }


        [Authorize(Roles = Role.WarehouseCrew)]
        [HttpPost]
        public ActionResult Loaded(Order order)
        {
            CommandDirectory.Instance.LoadOrder(WebSecurity.CurrentUserId, order);
            return Json(new { status = "success" });
        }
    }
}