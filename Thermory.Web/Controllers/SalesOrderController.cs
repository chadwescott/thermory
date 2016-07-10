using System;
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

        [Authorize(Role.InventoryMaster, Role.WarehouseCrew)]
        public ActionResult Package(Guid? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            var order = CommandDirectory.Instance.GetOrderById(id.Value);

            if (order == null)
                return RedirectToAction("Index");

            return order.PackagingType == null ? Review(id) : View(order);
        }

        [Authorize(Role.InventoryMaster, Role.WarehouseCrew)]
        [HttpPost]
        public JsonResult SavePackages(Guid orderId, PackageLumberLineItem[] lumberLineItems, PackageMiscellaneousLineItem[] miscLineItems)
        {
            CommandDirectory.Instance.SavePackages(WebSecurity.CurrentUserId, orderId, lumberLineItems, miscLineItems);
            return Json(new { status = "success" });
        }

        [Authorize(Role.InventoryMaster, Role.WarehouseCrew)]
        [HttpPost]
        public JsonResult UpdatePackages(Package[] packages)
        {
            CommandDirectory.Instance.UpdatePackages(packages);
            return Json(new { status = "success" });
        }
    }
}