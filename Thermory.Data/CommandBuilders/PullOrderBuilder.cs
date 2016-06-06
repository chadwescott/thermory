using System;
using Thermory.Data.Commands;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;

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

            order.MinutesToPull = minutesTaken;
            CreateInventoryTransaction(userId, order);

            order.OrderStatusId = DatabaseCommandDirectory.Instance.GetOrderStatusByEnum(OrderStatuses.Pulled).Id;
            Commands.Add(new SaveOrder(order));
        }
    }
}
