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
            var model = CreateOrderFormViewModel(OrderTypes.PurchaseOrder);
            return View("Form", model);
        }

        public ActionResult CreateSalesOrder()
        {
            var model = CreateOrderFormViewModel(OrderTypes.SalesOrder);
            return View("Form", model);
        }

        public ActionResult EditOrder(Guid id)
        {
            var order = CommandDirectory.GetOrderById(id);
            var model = CreateOrderFormViewModel(order);
            return View("Form", model);
        }

        [HttpPost]
        public ActionResult DeleteOrder(Guid orderId)
        {
            CommandDirectory.Instance.DeleteOrder(WebSecurity.CurrentUserId, orderId);
            return Json(new { status = "success" });
        }

        [HttpPost]
        public ActionResult SaveOrder(Guid orderId, OrderTypes orderType, ProductOrderQuantity[] lumberOrderQuantities,
            ProductOrderQuantity[] miscOrderQuantities)
        {
            var lumberLineItems = lumberOrderQuantities == null
                ? new OrderLumberLineItem[0]
                : lumberOrderQuantities.Select(l => new OrderLumberLineItem
                {
                    Id = l.Id,
                    OrderId = orderId,
                    LumberProductId = l.ProductId,
                    Quantity = l.Quantity
                }).ToArray();

            var miscLineItems = miscOrderQuantities == null
                ? new OrderMiscellaneousLineItem[0]
                : miscOrderQuantities.Select(m => new OrderMiscellaneousLineItem
                {
                    Id = m.Id,
                    OrderId = orderId,
                    MiscellaneousProductId = m.ProductId,
                    Quantity = m.Quantity
                }).ToArray();

            CommandDirectory.Instance.SaveOrder(WebSecurity.CurrentUserId, orderId, orderType, lumberLineItems, miscLineItems);
            return Json(new { status = "success" });
        }

        private OrderForm CreateOrderFormViewModel(Order order)
        {
            var model = new OrderForm
            {
                Order = order,
                OrderType = order.OrderType.OrderTypeEnum,
                LumberOrderForms = GetLumberOrderForms(),
                MiscellaneousOrderForms = GetMiscellaneousOrderForms()
            };

            foreach (var form in model.LumberOrderForms)
            {
                form.LumberLineItems = order.OrderLumberLineItems.Where(
                        li => li.LumberProduct.LumberType.LumberSubCategory.LumberCategoryId == form.LumberCategory.Id).ToList();
            }

            foreach (var form in model.MiscellaneousOrderForms)
            {
                form.MiscellaneousLineItems = order.OrderMiscellaneousLineItems.Where(
                        li => li.MiscellaneousProduct.MiscellaneousSubCategory.MiscellaneousCategory.Id == form.MiscellaneousCategory.Id).ToList();
            }

            return model;
        }

        private OrderForm CreateOrderFormViewModel(OrderTypes orderType)
        {
            return new OrderForm
            {
                Order = new Order(),
                OrderType = orderType,
                LumberOrderForms = GetLumberOrderForms(),
                MiscellaneousOrderForms = GetMiscellaneousOrderForms()
            };
        }

        private List<LumberOrderForm> GetLumberOrderForms()
        {
            return CommandDirectory.Instance.GetAllLumberCategories()
                .Select(c => new LumberOrderForm { LumberCategory = c })
                .ToList();
        }

        private static List<MiscellaneousOrderForm> GetMiscellaneousOrderForms()
        {
            return CommandDirectory.Instance.GetAllMiscellaneousCategories()
                .Select(c => new MiscellaneousOrderForm { MiscellaneousCategory = c })
                .ToList();
        }
    }
}
