using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Thermory.Business;
using Thermory.Domain.Constants;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;
using Thermory.Web.Models;
using WebMatrix.WebData;

namespace Thermory.Web.Controllers
{
    public abstract class OrderController : Controller
    {
        protected abstract OrderTypes OrderType { get; }
        protected abstract OrderStatuses InitialOrderStatus { get; }

        [Authorize]
        public ActionResult Review(Guid? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            var order = CommandDirectory.Instance.GetOrderById(id.Value);
            if (order == null)
                return RedirectToAction("Index");

            var orderStatuses = CommandDirectory.Instance.GetOrderStatusesByOrderTypeId(order.OrderTypeId);
            var inventoryTransactions = CommandDirectory.Instance.GetInventoryTransactionsByOrderId(order.Id);
            var model = new OrderReview { Order = order, OrderStatuses = orderStatuses, InventoryTransactions = inventoryTransactions };
            return View("Order/Review", model);
        }

        [Authorize(Roles = Role.InventoryMaster)]
        public ActionResult Create()
        {
            var model = CreateOrderFormViewModel();
            return View("Order/Form", model);
        }

        [Authorize(Roles = Role.InventoryMaster)]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            var order = CommandDirectory.Instance.GetOrderById(id.Value);
            if (order == null)
                return RedirectToAction("Index");

            var model = CreateOrderFormViewModel(order);
            if (order.OrderStatus.OrderStatusEnum == OrderStatuses.Deleted)
                return RedirectToAction("Review", new RouteValueDictionary { { "id", id } });
            return View("Order/Form", model);
        }

        [HttpPost]
        [Authorize(Roles = Role.InventoryMaster)]
        public ActionResult Delete(Order order)
        {
            CommandDirectory.Instance.DeleteOrder(WebSecurity.CurrentUserId, order);
            return Json(new { status = "success" });
        }

        [HttpPost]
        [Authorize(Roles = Role.InventoryMaster)]
        public ActionResult Save(Order order, ProductOrderQuantity[] lumberOrderQuantities, ProductOrderQuantity[] miscOrderQuantities)
        {
            var lumberLineItems = lumberOrderQuantities == null
                ? new OrderLumberLineItem[0]
                : lumberOrderQuantities.Select(l => new OrderLumberLineItem
                {
                    Id = l.Id,
                    OrderId = order.Id,
                    LumberProductId = l.ProductId,
                    Quantity = l.Quantity
                }).ToArray();

            var miscLineItems = miscOrderQuantities == null
                ? new OrderMiscellaneousLineItem[0]
                : miscOrderQuantities.Select(m => new OrderMiscellaneousLineItem
                {
                    Id = m.Id,
                    OrderId = order.Id,
                    MiscellaneousProductId = m.ProductId,
                    Quantity = m.Quantity
                }).ToArray();

            CommandDirectory.Instance.SaveOrder(WebSecurity.CurrentUserId, order, lumberLineItems, miscLineItems);
            return Json(order.Id);
        }

        private OrderForm CreateOrderFormViewModel(Order order)
        {
            var model = new OrderForm
            {
                Customers = CommandDirectory.Instance.GetAllCustomers(),
                Order = order,
                LumberOrderForms = GetLumberOrderForms(order.OrderType.OrderTypeEnum),
                MiscellaneousOrderForms = GetMiscellaneousOrderForms(order.OrderType.OrderTypeEnum),
                PackagingTypes = CommandDirectory.Instance.GetAllPackagingTypes()
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

        protected OrderForm CreateOrderFormViewModel()
        {
            return new OrderForm
            {
                Customers = CommandDirectory.Instance.GetAllCustomers(),
                Order =
                    new Order
                    {
                        OrderType = CommandDirectory.Instance.GetOrderTypeByOrderTypeEnum(OrderType),
                        OrderStatus = CommandDirectory.Instance.GetOrderStatusByOrderStatusEnum(InitialOrderStatus)
                    },
                LumberOrderForms = GetLumberOrderForms(OrderType),
                MiscellaneousOrderForms = GetMiscellaneousOrderForms(OrderType),
                PackagingTypes = CommandDirectory.Instance.GetAllPackagingTypes()
            };
        }

        private List<LumberOrderForm> GetLumberOrderForms(OrderTypes orderType)
        {
            return CommandDirectory.Instance.GetAllLumberCategories()
                .Select(c => new LumberOrderForm { LumberCategory = c, ValidateQuantityOnHand = orderType == OrderTypes.SalesOrder })
                .ToList();
        }

        private static List<MiscellaneousOrderForm> GetMiscellaneousOrderForms(OrderTypes orderType)
        {
            return CommandDirectory.Instance.GetAllMiscellaneousCategories()
                .Select(c => new MiscellaneousOrderForm { MiscellaneousCategory = c, ValidateQuantityOnHand = orderType == OrderTypes.SalesOrder })
                .ToList();
        }
        
        protected ActionResult GetOrderSummary(OrderTypes type)
        {
            var summary = CommandDirectory.Instance.GetOrderSummary(type);
            return Json(new { status = "success", records = summary });
        }
    }
}