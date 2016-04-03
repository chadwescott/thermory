using System;
using System.Collections.Generic;
using System.Linq;
using Thermory.Data.Commands;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;
using Thermory.Domain.Utils;

namespace Thermory.Data.CommandBuilders
{
    internal class EditOrderBuilder : OrderBuilder
    {
        public EditOrderBuilder(int userId, Guid orderId, Customer customer, OrderLumberLineItem[] lumberLineItems,
            OrderMiscellaneousLineItem[] miscLineItems)
        {
            var order = GetOrder(orderId);
            if (order.IsDeleted) return;

            var orderLumberLineItems = order.OrderLumberLineItems;
            var orderMiscLineItems = order.OrderMiscellaneousLineItems;
            
            ClearOrderLineItems(order);

            var transaction = CreateInventoryTransaction(userId, order.Id);
            var adjustmentMultiplier = AdjustmentMultiplier.GetByOrderType(order.OrderType.OrderTypeEnum);

            AddLumberProductQuantityAdjustmentCommands(transaction, orderLumberLineItems, lumberLineItems.ToList(), adjustmentMultiplier);
            AddMiscellaneousProductQuantityAdjustmentCommands(transaction, orderMiscLineItems, miscLineItems.ToList(), adjustmentMultiplier);

            AddCreatedLumberLineItemCommands(order, lumberLineItems);
            AddCreateMiscellaneousLineItemCommands(order, miscLineItems);

            if (customer.Name == null)
                customer = null;
            else
                Commands.Add(new SaveCustomer(customer));

            order.Customer = customer;
            Commands.Add(new SaveOrder(order));
        }

        private void ClearOrderLineItems(Order order)
        {
            AddDeleteLumberLineItemCommands(order);
            AddDeleteMiscellaneousLineItemCommands(order);
        }

        private void AddDeleteLumberLineItemCommands(Order order)
        {
            var builder = new DeleteLumberLineItemBuilder(order);
            Commands.AddRange(builder.Commands);
        }

        private void AddDeleteMiscellaneousLineItemCommands(Order order)
        {
            var builder = new DeleteMiscellaneousLineItemBuilder(order);
            Commands.AddRange(builder.Commands);
        }

        private void AddLumberProductQuantityAdjustmentCommands(InventoryTransaction transaction,
            List<OrderLumberLineItem> previousLineItems, List<OrderLumberLineItem> currentLineItems,
            int adjustmentMultiplier)
        {
            AddAddedLumberProductAddjustmentCommands(transaction, previousLineItems, currentLineItems,
                adjustmentMultiplier);
            AddEditedLumberProductAddjustmentCommands(transaction, previousLineItems, currentLineItems,
                adjustmentMultiplier);
            AddRemovedLumberProductAddjustmentCommands(transaction, previousLineItems, currentLineItems,
                adjustmentMultiplier);
        }

        private void AddAddedLumberProductAddjustmentCommands(InventoryTransaction transaction,
            IEnumerable<OrderLumberLineItem> previousLineItems, IEnumerable<OrderLumberLineItem> currentLineItems,
            int adjustmentMultiplier)
        {
            var adjustLumberProductQuantityCommands =
                currentLineItems.Where(
                    i => !previousLineItems.Any(p => p.LumberProductId == i.LumberProductId && p.OrderId == i.OrderId))
                    .Select(
                        i =>
                            new AdjustLumberProductQuantity(transaction, i.LumberProductId,
                                i.Quantity*adjustmentMultiplier));
            Commands.AddRange(adjustLumberProductQuantityCommands);
        }

        private void AddEditedLumberProductAddjustmentCommands(InventoryTransaction transaction,
            IEnumerable<OrderLumberLineItem> previousLineItems, List<OrderLumberLineItem> currentLineItems,
            int adjustmentMultiplier)
        {
            if (currentLineItems == null) return;

            foreach (var command in from currentLineItem in currentLineItems.Where(
                c =>
                    previousLineItems.Any(
                        p =>
                            p.LumberProductId == c.LumberProductId &&
                            p.OrderId == c.OrderId && p.Quantity != c.Quantity)) let previousLineItem = previousLineItems.Single(
                                p =>
                                    p.LumberProductId == currentLineItem.LumberProductId &&
                                    p.OrderId == currentLineItem.OrderId && p.Quantity != currentLineItem.Quantity)
                                    let delta = (currentLineItem.Quantity - previousLineItem.Quantity) * adjustmentMultiplier
                                    select new AdjustLumberProductQuantity(transaction, currentLineItem.LumberProductId, delta))
            {
                Commands.Add(command);
            }
        }

        private void AddRemovedLumberProductAddjustmentCommands(InventoryTransaction transaction,
            IEnumerable<OrderLumberLineItem> previousLineItems, IEnumerable<OrderLumberLineItem> currentLineItems,
            int adjustmentMultiplier)
        {
            var adjustLumberProductQuantityCommands =
                previousLineItems.Where(
                    i => !currentLineItems.Any(p => p.LumberProductId == i.LumberProductId && p.OrderId == i.OrderId))
                    .Select(
                        i =>
                            new AdjustLumberProductQuantity(transaction, i.LumberProductId,
                                -i.Quantity * adjustmentMultiplier));
            Commands.AddRange(adjustLumberProductQuantityCommands);
        }

        private void AddMiscellaneousProductQuantityAdjustmentCommands(InventoryTransaction transaction,
            List<OrderMiscellaneousLineItem> previousLineItems, List<OrderMiscellaneousLineItem> currentLineItems,
            int adjustmentMultiplier)
        {
            AddAddedMiscellaneousProductAddjustmentCommands(transaction, previousLineItems, currentLineItems,
                adjustmentMultiplier);
            AddEditedMiscellaneousProductAddjustmentCommands(transaction, previousLineItems, currentLineItems,
                adjustmentMultiplier);
            AddRemovedMiscellaneousProductAddjustmentCommands(transaction, previousLineItems, currentLineItems,
                adjustmentMultiplier);
        }

        private void AddAddedMiscellaneousProductAddjustmentCommands(InventoryTransaction transaction,
            IEnumerable<OrderMiscellaneousLineItem> previousLineItems, IEnumerable<OrderMiscellaneousLineItem> currentLineItems,
            int adjustmentMultiplier)
        {
            var adjustMiscellaneousProductQuantityCommands =
                currentLineItems.Where(
                    i => !previousLineItems.Any(p => p.MiscellaneousProductId == i.MiscellaneousProductId && p.OrderId == i.OrderId))
                    .Select(
                        i =>
                            new AdjustMiscellaneousProductQuantity(transaction, i.MiscellaneousProductId,
                                i.Quantity * adjustmentMultiplier));
            Commands.AddRange(adjustMiscellaneousProductQuantityCommands);
        }

        private void AddEditedMiscellaneousProductAddjustmentCommands(InventoryTransaction transaction,
            IEnumerable<OrderMiscellaneousLineItem> previousLineItems, List<OrderMiscellaneousLineItem> currentLineItems,
            int adjustmentMultiplier)
        {
            if (currentLineItems == null) return;

            foreach (var command in from currentLineItem in currentLineItems.Where(
                c =>
                    previousLineItems.Any(
                        p =>
                            p.MiscellaneousProductId == c.MiscellaneousProductId &&
                            p.OrderId == c.OrderId && p.Quantity != c.Quantity))
                                    let previousLineItem = previousLineItems.Single(
p =>
p.MiscellaneousProductId == currentLineItem.MiscellaneousProductId &&
p.OrderId == currentLineItem.OrderId && p.Quantity != currentLineItem.Quantity)
                                    let delta = (currentLineItem.Quantity - previousLineItem.Quantity) * adjustmentMultiplier
                                    select new AdjustMiscellaneousProductQuantity(transaction, currentLineItem.MiscellaneousProductId, delta))
            {
                Commands.Add(command);
            }
        }

        private void AddRemovedMiscellaneousProductAddjustmentCommands(InventoryTransaction transaction,
            IEnumerable<OrderMiscellaneousLineItem> previousLineItems, IEnumerable<OrderMiscellaneousLineItem> currentLineItems,
            int adjustmentMultiplier)
        {
            var adjustMiscellaneousProductQuantityCommands =
                previousLineItems.Where(
                    i => !currentLineItems.Any(p => p.MiscellaneousProductId == i.MiscellaneousProductId && p.OrderId == i.OrderId))
                    .Select(
                        i =>
                            new AdjustMiscellaneousProductQuantity(transaction, i.MiscellaneousProductId,
                                -i.Quantity * adjustmentMultiplier));
            Commands.AddRange(adjustMiscellaneousProductQuantityCommands);
        }

        protected override TransactionTypes TransactionType
        {
            get { return TransactionTypes.OrderEdit; }
        }
    }
}
