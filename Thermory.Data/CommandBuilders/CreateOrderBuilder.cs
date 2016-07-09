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

        private static void SetOrderStatus(Order order)
        {
            order.OrderStatusId = order.OrderType.OrderTypeEnum == OrderTypes.PurchaseOrder
                ? OrderStatusCache.GetByOrderStatusEnum(OrderStatuses.InTransit).Id
                : OrderStatusCache.GetByOrderStatusEnum(OrderStatuses.SentToWarehouse).Id;
        }

        protected override TransactionTypes TransactionType
        {
            get { return TransactionTypes.OrderCreate; }
        }

        protected void AddInventoryAdjustmentCommands(int userId, Order order, OrderLumberLineItem[] lumberLineItems, OrderMiscellaneousLineItem[] miscLineItems)
        {
            var transaction = CreateInventoryTransaction(userId, order);
            var adjustmentMultiplier = AdjustmentMultiplier.GetByOrderType(order.OrderType.OrderTypeEnum);

            var adjustLumberProductQuantityCommands =
                lumberLineItems.Select(
                    i =>
                        new AdjustLumberProductQuantity(transaction, i.LumberProductId, i.Quantity * adjustmentMultiplier,
                            order.ApplyInventoryQuantityChanges)).ToList();

            var adjustMiscellaneousProductQuantityCommands =
                miscLineItems.Select(
                    i =>
                        new AdjustMiscellaneousProductQuantity(transaction, i.MiscellaneousProductId,
                            i.Quantity * adjustmentMultiplier, order.ApplyInventoryQuantityChanges)).ToList();

            if (!adjustLumberProductQuantityCommands.Any() && !adjustMiscellaneousProductQuantityCommands.Any()) return;

            CreateInventoryTransactionCommand(transaction);
            Commands.AddRange(adjustLumberProductQuantityCommands);
            Commands.AddRange(adjustMiscellaneousProductQuantityCommands);
        }
    }
}