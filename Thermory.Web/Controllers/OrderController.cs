using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Thermory.Business;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;
using Thermory.Web.Models;
using WebMatrix.WebData;

namespace Thermory.Web.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreatePurchaseOrder()
        {
            var model = CreateOrderFormViewModel(OrderTypes.Purchase);
            return View("Form", model);
        }

        public ActionResult CreateSalesOrder()
        {
            var model = CreateOrderFormViewModel(OrderTypes.Sales);
            return View("Form", model);
        }

        public ActionResult EditOrder(Guid orderId)
        {
            var order = CommandDirectory.GetOrderById(orderId);
            var model = CreateOrderFormViewModel(order);
            return View("Form", model);
        }

        [HttpPost]
        public JsonResult SaveOrder(OrderTypes orderType, ProductOrderQuantity[] lumberOrderQuantities,
            ProductOrderQuantity[] miscOrderQuantities)
        {
            var lumberLineItems = lumberOrderQuantities == null
                ? new OrderLumberLineItem[0]
                : lumberOrderQuantities.Select(l => new OrderLumberLineItem
                {
                    LumberProduct = new LumberProduct {Id = l.Id},
                    Quantity = l.Quantity
                }).ToArray();

            var miscLineItems = miscOrderQuantities == null
                ? new OrderMiscellaneousLineItem[0]
                : miscOrderQuantities.Select(l => new OrderMiscellaneousLineItem
                {
                    MiscellaneousProduct = new MiscellaneousProduct {Id = l.Id},
                    Quantity = l.Quantity
                }).ToArray();

            CommandDirectory.Instance.CreateOrder(WebSecurity.CurrentUserId, orderType, lumberLineItems, miscLineItems);
            return Json(new { status = "success" });
        }

        private OrderFormModel CreateOrderFormViewModel(Order order)
        {
            var model = CreateOrderFormViewModel(order.OrderType.OrderTypeEnum);
            model.OrderQuantities = new List<ProductOrderQuantity>();
            model.OrderQuantities.AddRange(
                order.OrderLumberLineItems.Select(
                    li => new ProductOrderQuantity { Id = li.LumberProductId, Quantity = li.Quantity }));

            model.OrderQuantities.AddRange(
                order.OrderMiscellaneousLineItems.Select(
                    li => new ProductOrderQuantity { Id = li.MiscellaneousProductId, Quantity = li.Quantity }));

            return model;
        }

        private OrderFormModel CreateOrderFormViewModel(OrderTypes orderType)
        {
            return new OrderFormModel
            {
                OrderType = orderType,
                LumberCategories = CommandDirectory.Instance.GetAllLumberCategories(),
                MiscellaneousCategories = CommandDirectory.Instance.GetAllMiscellaneousCategories()
            };
        }
    }
}
