using System.Linq;
using Thermory.Data.Commands;
using Thermory.Domain;
using Thermory.Domain.Models;
using Thermory.Domain.Enums;

namespace Thermory.Data.CommandBuilders
{
    internal class CreateOrderBuilder : OrderBuilder
    {
        public CreateOrderBuilder(int userId, OrderTypes orderType, OrderLumberLineItem[] lumberLineItems,
            OrderMiscellaneousLineItem[] miscLineItems)
        {
            var orderTypeId = DatabaseCommandDirectory.Instance.GetOrderTypeIdByEnum(orderType);
            var order = new Order { OrderTypeId = orderTypeId };

            Commands.Add(new CreateOrder(order));

            AddCreatedLumberLineItemCommands(order, lumberLineItems);
            AddCreateMiscellaneousLineItemCommands(order, miscLineItems);

            var transaction = CreateInventoryTransaction(userId, order);

            var adjustmentMultiplier = AdjustmentMultiplier.GetByOrderType(orderType);

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