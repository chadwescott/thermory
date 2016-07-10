using System.Collections.Generic;
using System.Linq;
using Thermory.Data.Commands;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;

namespace Thermory.Data.CommandBuilders
{
    internal class ReceiveOrderBuilder : OrderBuilder
    {
        protected override TransactionTypes TransactionType
        {
            get { return TransactionTypes.OrderReceived; }
        }

        public ReceiveOrderBuilder(int userId, Order order)
        {
            order = GetOrder(order.Id);
            if (order == null || order.OrderStatus.OrderStatusEnum == OrderStatuses.Deleted) return;

            var transaction = MakeInventoryTransaction(userId, order);
            AddCreateInventoryTransactionCommand(transaction);
            AddLumberProductsToInventory(order.OrderLumberLineItems);
            AddMiscellaneousProductsToInventory(order.OrderMiscellaneousLineItems);

            order.OrderStatusId = DatabaseCommandDirectory.Instance.GetOrderStatusByEnum(OrderStatuses.Received).Id;
            Commands.Add(new SaveOrder(order));
        }

        private void AddLumberProductsToInventory(IEnumerable<OrderLumberLineItem> lineItems)
        {
            foreach (
                var command in
                    lineItems.Select(
                        currentLineItem => new AdjustLumberProductQuantity(null, currentLineItem.LumberProductId,
                            currentLineItem.Quantity, true)))
            {
                Commands.Add(command);
            }
        }

        private void AddMiscellaneousProductsToInventory(IEnumerable<OrderMiscellaneousLineItem> lineItems)
        {
            foreach (
                var command in
                    lineItems.Select(
                        currentLineItem =>
                            new AdjustMiscellaneousProductQuantity(null, currentLineItem.MiscellaneousProductId,
                                currentLineItem.Quantity, true)))
            {
                Commands.Add(command);
            }
        }
    }
}
