using System;
using System.Collections.Generic;
using System.Linq;
using Thermory.Data.Commands;
using Thermory.Domain;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;

namespace Thermory.Data.CommandBuilders
{
    internal class DeleteOrderBuilder : OrderBuilder
    {
        public DeleteOrderBuilder(int userId, Guid orderId)
        {
            var order = GetOrder(orderId);
            if (order.IsDeleted) return;

            order.IsDeleted = true;
            Commands.Add(new SaveOrder(order));

            var orderLumberLineItems = order.OrderLumberLineItems;
            var orderMiscLineItems = order.OrderMiscellaneousLineItems;

            var transaction = CreateInventoryTransaction(userId, order.Id);
            var adjustmentMultiplier = AdjustmentMultiplier.GetByOrderType(order.OrderType.OrderTypeEnum);

            AddLumberProductQuantityAdjustmentCommands(transaction, orderLumberLineItems, adjustmentMultiplier);
            AddMiscellaneousProductQuantityAdjustmentCommands(transaction, orderMiscLineItems, adjustmentMultiplier);
        }

        private void AddLumberProductQuantityAdjustmentCommands(InventoryTransaction transaction,
            IEnumerable<OrderLumberLineItem> lineItems, int adjustmentMultiplier)
        {
            var adjustLumberProductQuantityCommands =
                lineItems.Select(
                        i =>
                            new AdjustLumberProductQuantity(transaction, i.LumberProductId,
                                -i.Quantity * adjustmentMultiplier));
            Commands.AddRange(adjustLumberProductQuantityCommands);
        }

        private void AddMiscellaneousProductQuantityAdjustmentCommands(InventoryTransaction transaction,
            IEnumerable<OrderMiscellaneousLineItem> lineItems, int adjustmentMultiplier)
        {
            var adjustMiscellaneousProductQuantityCommands =
                lineItems.Select(
                    i =>
                        new AdjustMiscellaneousProductQuantity(transaction, i.MiscellaneousProductId,
                            -i.Quantity*adjustmentMultiplier));
            Commands.AddRange(adjustMiscellaneousProductQuantityCommands);
        }

        protected override TransactionTypes TransactionType
        {
            get { return TransactionTypes.OrderDelete; }
        }
    }
}
