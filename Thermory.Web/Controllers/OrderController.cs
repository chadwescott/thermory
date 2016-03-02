using System.Linq;
using System.Web.Mvc;
using Thermory.Business;
using Thermory.Domain;
using Thermory.Domain.Enums;
using Thermory.Web.Models;
using Thermory.Web.ViewModels;
using WebMatrix.WebData;

namespace Thermory.Web.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult CreatePurchaseOrder()
        {
            var model = CreateOrderFormViewModel(OrderTypes.Purchase);
            return View("Create", model);
        }

        public ActionResult CreateSalesOrder()
        {
            var model = CreateOrderFormViewModel(OrderTypes.Sales);
            return View("Create", model);
        }

        [HttpPost]
        public JsonResult CreateOrder(OrderTypes orderType, ProductOrderQuantity[] lumberOrderQuantities,
            ProductOrderQuantity[] miscOrderQuantities)
        {
            var lumberLineItems = lumberOrderQuantities == null
                ? new IOrderLumberLineItem[0]
                : lumberOrderQuantities.Select(l => new OrderLumberLineItem
                {
                    LumberProduct = new LumberProduct {Id = l.Id},
                    Quantity = l.Quantity
                }).ToArray();

            var miscLineItems = miscOrderQuantities == null
                ? new IOrderMiscellaneousLineItem[0]
                : miscOrderQuantities.Select(l => new OrderMiscellaneousLineItem
                {
                    MiscellaneousProduct = new MiscellaneousProduct {Id = l.Id},
                    Quantity = l.Quantity
                }).ToArray();

            CommandDirectory.Instance.CreateOrder(WebSecurity.CurrentUserId, orderType, lumberLineItems, miscLineItems);
            return Json(new { status = "success" });
        }

        private OrderFormViewModel CreateOrderFormViewModel(OrderTypes orderType)
        {
            return new OrderFormViewModel
            {
                OrderType = orderType,
                LumberCategories = CommandDirectory.Instance.GetAllLumberCategories(),
                MiscellaneousCategories = CommandDirectory.Instance.GetAllMiscellaneousCategories()
            };
        }
    }
}
