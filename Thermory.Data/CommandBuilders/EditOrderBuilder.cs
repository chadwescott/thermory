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
        protected override TransactionTypes TransactionType
        {
            get { return TransactionTypes.OrderEdit; }
        }

        public EditOrderBuilder(int userId, Order order, OrderLumberLineItem[] lumberLineItems, OrderMiscellaneousLineItem[] miscLineItems)
        {
            var dbOrder = GetOrder(order.Id);
            if (dbOrder == null || dbOrder.OrderStatus.OrderStatusEnum == OrderStatuses.Deleted) return;
            
            ClearOrderLineItems(dbOrder);

            AddInventoryAdjustmentCommands(userId, dbOrder, lumberLineItems, miscLineItems);

            AddCreateOrderLumberLineItemCommands(dbOrder, lumberLineItems);
            AddCreateOrderMiscellaneousLineItemCommands(dbOrder, miscLineItems);

            if (order.Customer == null || order.Customer.Name == null)
                dbOrder.Customer = null;
            else
            {
                dbOrder.Customer = order.Customer;
                Commands.Add(new SaveCustomer(order.Customer));
            }

            if (order.PackagingType == null || order.PackagingType.Name == null)
                dbOrder.PackagingType = null;
            else
            {
                dbOrder.PackagingType = order.PackagingType;
                Commands.Add(new SavePackagingType(dbOrder.PackagingType));
            }

            dbOrder.OrderNumber = order.OrderNumber;
            dbOrder.Notes = order.Notes;
            dbOrder.ShipDate = order.ShipDate;
            Commands.Add(new SaveOrder(dbOrder));
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

        private void AddInventoryAdjustmentCommands(int userId, Order order, OrderLumberLineItem[] lumberLineItems, OrderMiscellaneousLineItem[] miscLineItems)
        {
            var transaction = CreateInventoryTransaction(userId, order);
            var adjustmentMultiplier = AdjustmentMultiplier.GetByOrderType(order.OrderType.OrderTypeEnum);

            AddLumberProductQuantityAdjustmentCommands(transaction, order.OrderLumberLineItems, lumberLineItems.ToList(),
                adjustmentMultiplier, order.ApplyInventoryQuantityChanges);
            AddMiscellaneousProductQuantityAdjustmentCommands(transaction, order.OrderMiscellaneousLineItems,
                miscLineItems.ToList(), adjustmentMultiplier, order.ApplyInventoryQuantityChanges);
        }

        private void AddLumberProductQuantityAdjustmentCommands(InventoryTransaction transaction,
            List<OrderLumberLineItem> previousLineItems, List<OrderLumberLineItem> currentLineItems,
            int adjustmentMultiplier, bool applyInventoryQuantityChanges)
        {
            AddAddedLumberProductAddjustmentCommands(transaction, previousLineItems, currentLineItems,
                adjustmentMultiplier, applyInventoryQuantityChanges);
            AddEditedLumberProductAddjustmentCommands(transaction, previousLineItems, currentLineItems,
                adjustmentMultiplier, applyInventoryQuantityChanges);
            AddRemovedLumberProductAddjustmentCommands(transaction, previousLineItems, currentLineItems,
                adjustmentMultiplier, applyInventoryQuantityChanges);
        }

        private void AddAddedLumberProductAddjustmentCommands(InventoryTransaction transaction,
            IEnumerable<OrderLumberLineItem> previousLineItems, IEnumerable<OrderLumberLineItem> currentLineItems,
            int adjustmentMultiplier, bool applyInventoryQuantityChanges)
        {
            var adjustLumberProductQuantityCommands =
                currentLineItems.Where(
                    i => !previousLineItems.Any(p => p.LumberProductId == i.LumberProductId && p.OrderId == i.OrderId))
                    .Select(
                        i =>
                            new AdjustLumberProductQuantity(transaction, i.LumberProductId,
                                i.Quantity*adjustmentMultiplier, applyInventoryQuantityChanges));
            Commands.AddRange(adjustLumberProductQuantityCommands);
        }

        private void AddEditedLumberProductAddjustmentCommands(InventoryTransaction transaction,
            IEnumerable<OrderLumberLineItem> previousLineItems, IEnumerable<OrderLumberLineItem> currentLineItems,
            int adjustmentMultiplier, bool applyInventoryQuantityChanges)
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
                                    select new AdjustLumberProductQuantity(transaction, currentLineItem.LumberProductId, delta, applyInventoryQuantityChanges))
            {
                Commands.Add(command);
            }
        }

        private void AddRemovedLumberProductAddjustmentCommands(InventoryTransaction transaction,
            IEnumerable<OrderLumberLineItem> previousLineItems, IEnumerable<OrderLumberLineItem> currentLineItems,
            int adjustmentMultiplier, bool applyInventoryQuantityChanges)
        {
            var adjustLumberProductQuantityCommands =
                previousLineItems.Where(
                    i => !currentLineItems.Any(p => p.LumberProductId == i.LumberProductId && p.OrderId == i.OrderId))
                    .Select(
                        i =>
                            new AdjustLumberProductQuantity(transaction, i.LumberProductId,
                                -i.Quantity * adjustmentMultiplier, applyInventoryQuantityChanges));
            Commands.AddRange(adjustLumberProductQuantityCommands);
        }

        private void AddMiscellaneousProductQuantityAdjustmentCommands(InventoryTransaction transaction,
            List<OrderMiscellaneousLineItem> previousLineItems, List<OrderMiscellaneousLineItem> currentLineItems,
            int adjustmentMultiplier, bool applyInventoryQuantityChanges)
        {
            AddAddedMiscellaneousProductAddjustmentCommands(transaction, previousLineItems, currentLineItems,
                adjustmentMultiplier, applyInventoryQuantityChanges);
            AddEditedMiscellaneousProductAddjustmentCommands(transaction, previousLineItems, currentLineItems,
                adjustmentMultiplier, applyInventoryQuantityChanges);
            AddRemovedMiscellaneousProductAddjustmentCommands(transaction, previousLineItems, currentLineItems,
                adjustmentMultiplier, applyInventoryQuantityChanges);
        }

        private void AddAddedMiscellaneousProductAddjustmentCommands(InventoryTransaction transaction,
            IEnumerable<OrderMiscellaneousLineItem> previousLineItems, IEnumerable<OrderMiscellaneousLineItem> currentLineItems,
            int adjustmentMultiplier, bool applyInventoryQuantityChanges)
        {
            var adjustMiscellaneousProductQuantityCommands =
                currentLineItems.Where(
                    i => !previousLineItems.Any(p => p.MiscellaneousProductId == i.MiscellaneousProductId && p.OrderId == i.OrderId))
                    .Select(
                        i =>
                            new AdjustMiscellaneousProductQuantity(transaction, i.MiscellaneousProductId,
                                i.Quantity * adjustmentMultiplier, applyInventoryQuantityChanges));
            Commands.AddRange(adjustMiscellaneousProductQuantityCommands);
        }

        private void AddEditedMiscellaneousProductAddjustmentCommands(InventoryTransaction transaction,
            IEnumerable<OrderMiscellaneousLineItem> previousLineItems, List<OrderMiscellaneousLineItem> currentLineItems,
            int adjustmentMultiplier, bool applyInventoryQuantityChanges)
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
                let delta = (currentLineItem.Quantity - previousLineItem.Quantity)*adjustmentMultiplier
                select
                    new AdjustMiscellaneousProductQuantity(transaction, currentLineItem.MiscellaneousProductId, delta,
                        applyInventoryQuantityChanges))
            {
                Commands.Add(command);
            }
        }

        private void AddRemovedMiscellaneousProductAddjustmentCommands(InventoryTransaction transaction,
            IEnumerable<OrderMiscellaneousLineItem> previousLineItems, IEnumerable<OrderMiscellaneousLineItem> currentLineItems,
            int adjustmentMultiplier, bool applyInventoryQuantityChanges)
        {
            var adjustMiscellaneousProductQuantityCommands =
                previousLineItems.Where(
                    i => !currentLineItems.Any(p => p.MiscellaneousProductId == i.MiscellaneousProductId && p.OrderId == i.OrderId))
                    .Select(
                        i =>
                            new AdjustMiscellaneousProductQuantity(transaction, i.MiscellaneousProductId,
                                -i.Quantity * adjustmentMultiplier, applyInventoryQuantityChanges));
            Commands.AddRange(adjustMiscellaneousProductQuantityCommands);
        }
    }
}
