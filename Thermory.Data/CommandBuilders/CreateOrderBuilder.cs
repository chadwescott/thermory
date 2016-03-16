using System.Linq;
using Thermory.Data.Commands;
using Thermory.Data.Constants;
using Thermory.Domain.Models;
using Thermory.Domain.Enums;

namespace Thermory.Data.CommandBuilders
{
    internal class CreateOrderBuilder : CommandBuilder
    {
        public CreateOrderBuilder(int userId, OrderTypes orderType, OrderLumberLineItem[] lumberLineItems,
            OrderMiscellaneousLineItem[] miscLineItems)
        {
            var orderTypeId = DatabaseCommandDirectory.Instance.GetOrderTypeyEnum(orderType);
            var adjustmentMultiplier = orderType.ToString() == OrderTypeNames.PurchaseOrder ? 1 : -1;
            var order = new Order { OrderTypeId = orderTypeId };

            Commands.Add(new CreateOrder(order));

            var createOrderLumberLinesCommands =
                lumberLineItems.Select(i => new CreateOrderLumberLineItem(order, i.LumberProduct.Id, i.Quantity * adjustmentMultiplier));
            Commands.AddRange(createOrderLumberLinesCommands);

            var createOrderMisceLinesCommands =
                miscLineItems.Select(i => new CreateOrderMiscellaneousLineItem(order, i.MiscellaneousProduct.Id, i.Quantity * adjustmentMultiplier));
            Commands.AddRange(createOrderMisceLinesCommands);
            
            var transactionTypeId =
                DatabaseCommandDirectory.Instance.GetTransactionTypeyEnum(TransactionTypes.OrderCreate);
            
            var transaction = new InventoryTransaction { UserId = userId, Order = order, TransactionTypeId = transactionTypeId };
            var createInventoryTransactionCommand = new CreateInventoryTransaction(transaction);
            Commands.Add(createInventoryTransactionCommand);

            var adjustLumberProductQuantityCommands =
                lumberLineItems.Select(i => new AdjustLumberProductQuantity(transaction, i.LumberProduct.Id, i.Quantity * adjustmentMultiplier));
            Commands.AddRange(adjustLumberProductQuantityCommands);

            var adjustMiscellaneousProductQuantityCommands =
                miscLineItems.Select(i => new AdjustMiscellaneousProductQuantity(transaction, i.MiscellaneousProduct.Id, i.Quantity * adjustmentMultiplier));
            Commands.AddRange(adjustMiscellaneousProductQuantityCommands);
        }
    }
}