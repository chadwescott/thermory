using System.Linq;
using Thermory.Data.Commands;
using Thermory.Domain.Models;
using Thermory.Domain.Enums;
using Thermory.Domain.Utils;

namespace Thermory.Data.CommandBuilders
{
    internal class CreateOrderBuilder : OrderBuilder
    {
        public CreateOrderBuilder(int userId, Order order,
            OrderLumberLineItem[] lumberLineItems, OrderMiscellaneousLineItem[] miscLineItems)
        {
            AddCustomerSaveCommand(order);
            AddPackagingTypeSaveCommand(order);
            SetOrderStatus(order);
            Commands.Add(new SaveOrder(order));

            AddCreateOrderLumberLineItemCommands(order, lumberLineItems);
            AddCreateOrderMiscellaneousLineItemCommands(order, miscLineItems);

            AddInventoryAdjustmentCommands(userId, order, lumberLineItems, miscLineItems);
        }

        private void AddCustomerSaveCommand(Order order)
        {
            if (order.Customer == null || order.Customer.Name == null)
                order.Customer = null;
            else
                Commands.Add(new SaveCustomer(order.Customer));
        }

        private void AddPackagingTypeSaveCommand(Order order)
        {
            if (order.PackagingType == null || order.PackagingType.Name == null)
                order.PackagingType = null;
            else
                Commands.Add(new SavePackagingType(order.PackagingType));
        }

        private static void SetOrderStatus(Order order)
        {
            order.OrderStatusId = order.OrderType.OrderTypeEnum == OrderTypes.PurchaseOrder
                ? OrderStatusCache.GetByOrderStatusEnum(OrderStatuses.InTransit).Id
                : OrderStatusCache.GetByOrderStatusEnum(OrderStatuses.SentToWarehouse).Id;
        }

        private void AddInventoryAdjustmentCommands(int userId, Order order, OrderLumberLineItem[] lumberLineItems, OrderMiscellaneousLineItem[] miscLineItems)
        {
            var transaction = CreateInventoryTransaction(userId, order);

            var adjustmentMultiplier = AdjustmentMultiplier.GetByOrderType(order.OrderType.OrderTypeEnum);

            var adjustLumberProductQuantityCommands =
                lumberLineItems.Select(
                    i =>
                        new AdjustLumberProductQuantity(transaction, i.LumberProductId, i.Quantity*adjustmentMultiplier,
                            order.ApplyInventoryQuantityChanges));
            Commands.AddRange(adjustLumberProductQuantityCommands);

            var adjustMiscellaneousProductQuantityCommands =
                miscLineItems.Select(
                    i =>
                        new AdjustMiscellaneousProductQuantity(transaction, i.MiscellaneousProductId,
                            i.Quantity*adjustmentMultiplier, order.ApplyInventoryQuantityChanges));
            Commands.AddRange(adjustMiscellaneousProductQuantityCommands);
        }

        protected override TransactionTypes TransactionType
        {
            get { return TransactionTypes.OrderCreate; }
        }
    }
}