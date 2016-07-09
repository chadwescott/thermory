using System;
using Thermory.Data.Commands;
using Thermory.Domain.Enums;

namespace Thermory.Data.CommandBuilders
{
    internal class LoadOrderBuilder : OrderBuilder
    {
        protected override TransactionTypes TransactionType
        {
            get { return TransactionTypes.OrderLoaded; }
        }

        public LoadOrderBuilder(int userId, Guid orderId, int minutesTaken)
        {
            var order = GetOrder(orderId);
            if (order == null || order.OrderStatus.OrderStatusEnum == OrderStatuses.Deleted) return;
            
            var transaction = MakeInventoryTransaction(userId, order);
            AddCreateInventoryTransactionCommand(transaction);

            order.OrderStatusId = DatabaseCommandDirectory.Instance.GetOrderStatusByEnum(OrderStatuses.Loaded).Id;
            order.MinutesToLoad = minutesTaken;
            Commands.Add(new SaveOrder(order));
        }
    }
}
