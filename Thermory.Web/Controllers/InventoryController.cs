using System;
using System.Linq;
using System.Web.Mvc;
using Thermory.Business;
using Thermory.Domain.Constants;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;
using Thermory.Web.Models;
using WebMatrix.WebData;

namespace Thermory.Web.Controllers
{
    public class InventoryController : Controller
    {
        [Attributes.Authorize(Role.InventoryMaster, Role.InventoryViewer, Role.WarehouseCrew)]
        public ActionResult Index()
        {
            var model = new InventoryModel
            {
                LumberCategories = CommandDirectory.Instance.GetAllLumberCategories(),
                MiscellaneousCategories = CommandDirectory.Instance.GetAllMiscellaneousCategories()
            };
            return View(model);
        }

        [Attributes.Authorize(Roles = Role.InventoryMaster)]
        public ActionResult Audit()
        {
            var model = new InventoryModel
            {
                LumberCategories = CommandDirectory.Instance.GetAllLumberCategories(),
                MiscellaneousCategories = CommandDirectory.Instance.GetAllMiscellaneousCategories()
            };
            return View(model);
        }

        [HttpPost]
        [Attributes.Authorize(Roles = Role.InventoryMaster)]
        public JsonResult Audit(LumberProduct[] lumberProducts, MiscellaneousProduct[] miscProducts)
        {
            var lumberInventory = lumberProducts ?? new LumberProduct[0];
            var miscInventory = miscProducts ?? new MiscellaneousProduct[0];
            CommandDirectory.Instance.UpdateProductInventory(WebSecurity.CurrentUserId, TransactionTypes.Audit,
                lumberInventory, miscInventory);
            return Json(new { status = "success" });
        }

        [HttpPost]
        [Attributes.Authorize(Roles = Role.InventoryMaster)]
        public JsonResult GetLumberTypeHistory(Guid lumberTypeId)
        {
            var summary = CommandDirectory.Instance.GetLumberTypeHistory(lumberTypeId);
            var x = summary.Select(h =>
                new
                {
                    h.Delta,
                    h.PreviousQuantity,
                    h.NewQuantity,
                    h.InventoryTransaction.CreatedOn,
                    h.InventoryTransaction.CreatedBy.FullName,
                    h.InventoryTransaction.TransactionType.Name,
                    Order = h.InventoryTransaction.Order == null ? null : new Order
                    {
                        Id = h.InventoryTransaction.Order.Id,
                        OrderNumber = h.InventoryTransaction.Order.OrderNumber,
                        OrderType = h.InventoryTransaction.Order.OrderType
                    },
                    h.LumberProduct
                }).ToList();
            var result = Json(new { status = "success", records = x });
            return result;
        }
    }
}
