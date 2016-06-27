using System.Collections.Generic;
using System.Linq;
using Thermory.Data.Commands;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;
using Thermory.Domain.Utils;

namespace Thermory.Data.CommandBuilders
{
    internal class DeleteOrderBuilder : OrderBuilder
    {
        public DeleteOrderBuilder(int userId, Order order)
        {
            order = GetOrder(order.Id);
            if (order.OrderStatus.OrderStatusEnum == OrderStatuses.Deleted) return;

            var orderLumberLineItems = order.OrderLumberLineItems;
            var orderMiscLineItems = order.OrderMiscellaneousLineItems;

            var transaction = CreateInventoryTransaction(userId, order);
            var adjustmentMultiplier = AdjustmentMultiplier.GetByOrderType(order.OrderType.OrderTypeEnum);

            AddLumberProductQuantityAdjustmentCommands(transaction, orderLumberLineItems, adjustmentMultiplier, order.ApplyInventoryQuantityChanges);
            AddMiscellaneousProductQuantityAdjustmentCommands(transaction, orderMiscLineItems, adjustmentMultiplier, order.ApplyInventoryQuantityChanges);

            order.OrderStatus = DatabaseCommandDirectory.Instance.GetOrderStatusByEnum(OrderStatuses.Deleted, order.OrderTypeId);
            order.OrderStatusId = order.OrderStatus.Id;
            Commands.Add(new SaveOrder(order));
        }

        private void AddLumberProductQuantityAdjustmentCommands(InventoryTransaction transaction,
            IEnumerable<OrderLumberLineItem> lineItems, int adjustmentMultiplier, bool applyInventoryQuantityChanges)
        {
            var adjustLumberProductQuantityCommands =
                lineItems.Select(
                        i =>
                            new AdjustLumberProductQuantity(transaction, i.LumberProductId,
                                -i.Quantity * adjustmentMultiplier, applyInventoryQuantityChanges));
            Commands.AddRange(adjustLumberProductQuantityCommands);
        }

        private void AddMiscellaneousProductQuantityAdjustmentCommands(InventoryTransaction transaction,
            IEnumerable<OrderMiscellaneousLineItem> lineItems, int adjustmentMultiplier, bool applyInventoryQuantityChanges)
        {
            var adjustMiscellaneousProductQuantityCommands =
                lineItems.Select(
                    i =>
                        new AdjustMiscellaneousProductQuantity(transaction, i.MiscellaneousProductId,
                            -i.Quantity*adjustmentMultiplier, applyInventoryQuantityChanges));
            Commands.AddRange(adjustMiscellaneousProductQuantityCommands);
        }

        protected override TransactionTypes TransactionType
        {
            get { return TransactionTypes.OrderDelete; }
        }
    }
}
