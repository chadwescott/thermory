using System;
using Thermory.Domain;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;

namespace Thermory.Data.CommandBuilders
{
    internal class EditOrderBuilder : OrderBuilder
    {
        public EditOrderBuilder(int userId, Guid orderId, OrderLumberLineItem[] lumberLineItems,
            OrderMiscellaneousLineItem[] miscLineItems)
        {
            var order = GetOrder(orderId);

            var orderLumberLineItems = order.OrderLumberLineItems;
            var orderMiscLineItems = order.OrderMiscellaneousLineItems;
            
            ClearOrderLineItems(order);

            var transaction = CreateInventoryTransaction(userId, order.Id);
            var adjustmentMultiplier = AdjustmentMultiplier.GetByOrderType(order.OrderType.OrderTypeEnum);

            //var adjustLumberProductQuantityCommands =
            //    lumberLineItems.Select(i => new AdjustLumberProductQuantity(transaction, i.LumberProduct.Id, i.Quantity * adjustmentMultiplier));
            //Commands.AddRange(adjustLumberProductQuantityCommands);

            //var adjustMiscellaneousProductQuantityCommands =
            //    miscLineItems.Select(i => new AdjustMiscellaneousProductQuantity(transaction, i.MiscellaneousProduct.Id, i.Quantity * adjustmentMultiplier));
            //Commands.AddRange(adjustMiscellaneousProductQuantityCommands);

            AddCreatedLumberLineItemCommands(order, lumberLineItems);
            AddCreateMiscellaneousLineItemCommands(order, miscLineItems);
        }

        private void ClearOrderLineItems(Order order)
        {
            AddDeleteLumberLineItemCommands(order);
            AddDeleteMiscellaneousLineItemCommands(order);
        }

        protected void AddDeleteLumberLineItemCommands(Order order)
        {
            var builder = new DeleteLumberLineItemBuilder(order);
            Commands.AddRange(builder.Commands);
        }

        protected void AddDeleteMiscellaneousLineItemCommands(Order order)
        {
            var builder = new DeleteMiscellaneousLineItemBuilder(order);
            Commands.AddRange(builder.Commands);
        }

        protected override TransactionTypes TransactionType
        {
            get { return TransactionTypes.OrderEdit; }
        }
    }
}
