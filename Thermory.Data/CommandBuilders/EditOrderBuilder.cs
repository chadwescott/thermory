using System;
using System.Collections.Generic;
using System.Linq;
using Thermory.Data.Commands;
using Thermory.Data.Extensions;
using Thermory.Data.Tools;
using Thermory.Domain;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;

namespace Thermory.Data.CommandBuilders
{
    internal class EditOrderBuilder : CommandBuilder
    {
        public EditOrderBuilder(int userId, Guid orderId, OrderLumberLineItem[] lumberLineItems,
            OrderMiscellaneousLineItem[] miscLineItems)
        {
            var order = GetOrder(orderId);
            var adjustmentMultiplier = GetAdjustmentMultiplier(order);
            AddCreateLumberLineItemCommands(order, lumberLineItems);
            AddEditLumberLineItemCommands(order, lumberLineItems);
            AddDeleteLumberLineItemCommands(order, lumberLineItems);
            AddCreateMiscellaneousLineItemCommands(order, miscLineItems);
            AddEditMiscellaneousLineItemCommands(order, miscLineItems);
            AddDeleteMiscellaneousLineItemCommands(order, miscLineItems);

            //TODO: Finish edit order builder

            var transactionTypeId =
                DatabaseCommandDirectory.Instance.GetTransactionTypeyEnum(TransactionTypes.OrderEdit);

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

        private Order GetOrder(Guid orderId)
        {
            var getOrderCommand = new GetOrderById(orderId);
            getOrderCommand.Execute();
            return getOrderCommand.Result;
        }

        private int GetAdjustmentMultiplier(Order order)
        {
            return AdjustmentMultiplier.GetByOrderType(order.OrderType.OrderTypeEnum);
        }

        private void AddCreateLumberLineItemCommands(Order order, IEnumerable<OrderLumberLineItem> updatedLumberLineItems)
        {
            var createdLumberLineItems = OrderHelper.GetCreatedOrderLumberLineItems(order, updatedLumberLineItems);
            var createOrderLumberLinesCommands = createdLumberLineItems.MakeCreateOrderLumberLineItemCommands(order);
            Commands.AddRange(createOrderLumberLinesCommands);
        }

        private void AddEditLumberLineItemCommands(Order order, IEnumerable<OrderLumberLineItem> updatedLumberLineItems)
        {
            var editedLumberLineItems = OrderHelper.GetEditedOrderLumberLineItems(order, updatedLumberLineItems);
            var editOrderLumberLinesCommands = editedLumberLineItems.MakeEditOrderLumberLineItemCommands();
            Commands.AddRange(editOrderLumberLinesCommands);
        }

        private void AddDeleteLumberLineItemCommands(Order order, IEnumerable<OrderLumberLineItem> updatedLumberLineItems)
        {
            var deletedLumberLineItems = OrderHelper.GetDeletedOrderLumberLineItems(order, updatedLumberLineItems);
            var deleteOrderLumberLinesCommands = deletedLumberLineItems.MakeDeleteOrderLumberLineItemCommands();
            Commands.AddRange(deleteOrderLumberLinesCommands);
        }

        //TODO: Finish these methods
        private void AddCreateMiscellaneousLineItemCommands(Order order, IEnumerable<OrderMiscellaneousLineItem> updatedMiscellaneousLineItems)
        {


            //var createOrderMiscLinesCommands =
            //    miscLineItems.Select(i => new CreateOrderMiscellaneousLineItem(order, i.MiscellaneousProduct.Id, i.Quantity));
            //Commands.AddRange(createOrderMiscLinesCommands);

            //var createdMiscellaneousLineItems = OrderHelper.GetCreatedOrderMiscellaneousLineItems(order, updatedMiscellaneousLineItems);
            //var createOrderMiscellaneousLinesCommands = createdMiscellaneousLineItems.MakeCreateOrderMiscellaneousLineItemCommands(order);
            //Commands.AddRange(createOrderMiscellaneousLinesCommands);
        }

        private void AddEditMiscellaneousLineItemCommands(Order order, IEnumerable<OrderMiscellaneousLineItem> updatedMiscellaneousLineItems)
        {
            //var editedMiscellaneousLineItems = OrderHelper.GetEditedOrderMiscellaneousLineItems(order, updatedMiscellaneousLineItems);
            //var editOrderMiscellaneousLinesCommands = editedMiscellaneousLineItems.MakeEditOrderMiscellaneousLineItemCommands();
            //Commands.AddRange(editOrderMiscellaneousLinesCommands);
        }

        private void AddDeleteMiscellaneousLineItemCommands(Order order, IEnumerable<OrderMiscellaneousLineItem> updatedMiscellaneousLineItems)
        {
            //var deletedMiscellaneousLineItems = OrderHelper.GetDeletedOrderMiscellaneousLineItems(order, updatedMiscellaneousLineItems);
            //var deleteOrderMiscellaneousLinesCommands = deletedMiscellaneousLineItems.MakeDeleteOrderMiscellaneousLineItemCommands();
            //Commands.AddRange(deleteOrderMiscellaneousLinesCommands);
        }
    }
}
