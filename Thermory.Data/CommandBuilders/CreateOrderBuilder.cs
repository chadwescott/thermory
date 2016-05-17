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
            if (order.Customer.Name == null)
                order.Customer = null;
            else
                Commands.Add(new SaveCustomer(order.Customer));

            if (order.PackagingType == null || order.PackagingType.Name == null)
                order.PackagingType = null;
            else
                Commands.Add(new SavePackagingType(order.PackagingType));

            order.OrderStatusId = order.OrderType.OrderTypeEnum == OrderTypes.PurchaseOrder
                ? OrderStatusCache.GetByOrderStatusEnum(OrderStatuses.InTransit).Id
                : OrderStatusCache.GetByOrderStatusEnum(OrderStatuses.SentToWarehouse).Id;
            Commands.Add(new SaveOrder(order));

            AddCreatedLumberLineItemCommands(order, lumberLineItems);
            AddCreateMiscellaneousLineItemCommands(order, miscLineItems);

            var transaction = CreateInventoryTransaction(userId, order);

            var adjustmentMultiplier = AdjustmentMultiplier.GetByOrderType(order.OrderType.OrderTypeEnum);

            var adjustLumberProductQuantityCommands =
                lumberLineItems.Select(i => new AdjustLumberProductQuantity(transaction, i.LumberProductId, i.Quantity * adjustmentMultiplier));
            Commands.AddRange(adjustLumberProductQuantityCommands);

            var adjustMiscellaneousProductQuantityCommands =
                miscLineItems.Select(i => new AdjustMiscellaneousProductQuantity(transaction, i.MiscellaneousProductId, i.Quantity * adjustmentMultiplier));
            Commands.AddRange(adjustMiscellaneousProductQuantityCommands);
        }

        protected override TransactionTypes TransactionType
        {
            get { return TransactionTypes.OrderCreate; }
        }
    }
}