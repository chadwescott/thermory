using Thermory.Data.Commands;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;

namespace Thermory.Data.CommandBuilders
{
    internal class WarehouseReceviedOrderBuilder : OrderBuilder
    {
        protected override TransactionTypes TransactionType
        {
            get { return TransactionTypes.OrderWarehouseReceived; }
        }

        public WarehouseReceviedOrderBuilder(int userId, Order order)
        {
            order = GetOrder(order.Id);
            if (order == null || order.OrderStatus.OrderStatusEnum == OrderStatuses.Deleted) return;

            CreateInventoryTransaction(userId, order);

            order.OrderStatusId = DatabaseCommandDirectory.Instance.GetOrderStatusByEnum(OrderStatuses.WarehouseReceived).Id;
            Commands.Add(new SaveOrder(order));
        }
    }
}
