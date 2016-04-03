using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
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

        public ActionResult Review(Guid? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            var order = CommandDirectory.Instance.GetOrderById(id.Value);
            if (order == null)
                return RedirectToAction("Index");

            var inventoryTransactions = CommandDirectory.Instance.GetInventoryTransactionsByOrderId(order.Id);
            var model = new OrderReview { Order = order, InventoryTransactions = inventoryTransactions };
            return View(model);
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

        public ActionResult EditOrder(Guid? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            var order = CommandDirectory.Instance.GetOrderById(id.Value);
            if (order == null)
                return RedirectToAction("Index");

            var model = CreateOrderFormViewModel(order);
            if (order.IsDeleted)
                return RedirectToAction("Review", new RouteValueDictionary { { "id", id } });
            return View("Form", model);
        }

        [HttpPost]
        public ActionResult DeleteOrder(Guid orderId)
        {
            CommandDirectory.Instance.DeleteOrder(WebSecurity.CurrentUserId, orderId);
            return Json(new { status = "success" });
        }

        [HttpPost]
        public ActionResult SaveOrder(Guid orderId, OrderTypes orderType, Customer customer, ProductOrderQuantity[] lumberOrderQuantities,
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

            CommandDirectory.Instance.SaveOrder(WebSecurity.CurrentUserId, orderId, orderType, customer, lumberLineItems, miscLineItems);
            return Json(new { status = "success" });
        }

        private OrderForm CreateOrderFormViewModel(Order order)
        {
            var model = new OrderForm
            {
                Customers = CommandDirectory.Instance.GetAllCustomers(),
                Order = order,
                OrderType = order.OrderType.OrderTypeEnum,
                LumberOrderForms = GetLumberOrderForms(order.OrderType.OrderTypeEnum),
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
                Customers = CommandDirectory.Instance.GetAllCustomers(),
                Order = new Order(),
                OrderType = orderType,
                LumberOrderForms = GetLumberOrderForms(orderType),
                MiscellaneousOrderForms = GetMiscellaneousOrderForms()
            };
        }

        private List<LumberOrderForm> GetLumberOrderForms(OrderTypes orderType)
        {
            return CommandDirectory.Instance.GetAllLumberCategories()
                .Select(c => new LumberOrderForm { LumberCategory = c, ValidateQuantityOnHand = orderType == OrderTypes.SalesOrder })
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
