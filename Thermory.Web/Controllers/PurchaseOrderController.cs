using System.Web.Mvc;
using Thermory.Business;
using Thermory.Domain.Constants;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;
using WebMatrix.WebData;

namespace Thermory.Web.Controllers
{
    public class PurchaseOrderController : OrderController
    {
        protected override OrderTypes OrderType
        {
            get { return OrderTypes.PurchaseOrder; }
        }

        protected override OrderStatuses InitialOrderStatus
        {
            get { return OrderStatuses.InTransit; }
        }

        [Attributes.Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Attributes.Authorize(Roles = Role.InventoryMaster)]
        [HttpPost]
        public ActionResult Receive(Order order)
        {
            CommandDirectory.Instance.ReceiveOrder(WebSecurity.CurrentUserId, order);
            return Json(new { status = "success" });
        }

        [HttpPost]
        [Attributes.Authorize(Role.InventoryMaster)]
        public ActionResult GetOrderSummary()
        {
            return GetOrderSummary(OrderTypes.PurchaseOrder);
        }
    }
}