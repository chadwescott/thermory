using System;
using Thermory.Data.Commands;
using Thermory.Domain.Enums;

namespace Thermory.Data.CommandBuilders
{
    internal class PullOrderBuilder : OrderBuilder
    {
        protected override TransactionTypes TransactionType
        {
            get { return TransactionTypes.OrderPulled; }
        }

        public PullOrderBuilder(int userId, Guid orderId, int minutesTaken)
        {
            var order = GetOrder(orderId);
            if (order == null || order.OrderStatus.OrderStatusEnum == OrderStatuses.Deleted) return;

            var transaction = MakeInventoryTransaction(userId, order);
            AddCreateInventoryTransactionCommand(transaction);

            order.OrderStatusId = DatabaseCommandDirectory.Instance.GetOrderStatusByEnum(OrderStatuses.Pulled).Id;
            order.MinutesToPull = minutesTaken;
            Commands.Add(new SaveOrder(order));
        }
    }
}
