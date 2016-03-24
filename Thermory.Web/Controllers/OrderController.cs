using System;
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
        public ActionResult SaveOrder(Guid orderId, OrderTypes orderType, ProductOrderQuantity[] lumberOrderQuantities,
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

            CommandDirectory.Instance.SaveOrder(WebSecurity.CurrentUserId, orderId, orderType, lumberLineItems, miscLineItems);
            return Json(new { status = "success" });
        }

        private OrderForm CreateOrderFormViewModel(Order order)
        {
            var model = CreateOrderFormViewModel(order.OrderType.OrderTypeEnum);
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
                LumberOrderForms = CommandDirectory.Instance.GetAllLumberCategories().Select(c => new LumberOrderForm{ LumberCategory = c }).ToList(),
                MiscellaneousOrderForms = CommandDirectory.Instance.GetAllMiscellaneousCategories().Select(c => new MiscellaneousOrderForm { MiscellaneousCategory = c }).ToList()
            };
        }
    }
}
