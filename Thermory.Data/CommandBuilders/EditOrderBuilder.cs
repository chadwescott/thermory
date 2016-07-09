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

            dbOrder.OrderNumber = order.OrderNumber;
            dbOrder.Notes = order.Notes;
            dbOrder.ShipDate = order.ShipDate;
            dbOrder.Customer = order.Customer;
            dbOrder.PackagingType = order.PackagingType;
            
            ClearOrderLineItems(dbOrder);

            AddInventoryAdjustmentCommands(userId, dbOrder, lumberLineItems, miscLineItems);
            AddCreateOrderLumberLineItemCommands(dbOrder, lumberLineItems);
            AddCreateOrderMiscellaneousLineItemCommands(dbOrder, miscLineItems);
            AddCustomerSaveCommand(dbOrder);
            AddPackagingTypeSaveCommand(dbOrder);

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
            var transaction = MakeInventoryTransaction(userId, order);
            var adjustmentMultiplier = AdjustmentMultiplier.GetByOrderType(order.OrderType.OrderTypeEnum);

            var lumberQuantityUpdateCommands = GetLumberProductQuantityAdjustmentCommands(transaction,
                order.OrderLumberLineItems, lumberLineItems.ToList(), adjustmentMultiplier,
                order.ApplyInventoryQuantityChanges);

            var miscQuantityUpdateCommands = GetMiscellaneousProductQuantityAdjustmentCommands(transaction, order.OrderMiscellaneousLineItems,
                miscLineItems.ToList(), adjustmentMultiplier, order.ApplyInventoryQuantityChanges);

            if (!lumberQuantityUpdateCommands.Any() && !miscQuantityUpdateCommands.Any()) return;

            AddCreateInventoryTransactionCommand(transaction);
            Commands.AddRange(lumberQuantityUpdateCommands);
            Commands.AddRange(miscQuantityUpdateCommands);
        }

        private List<DatabaseCommand> GetLumberProductQuantityAdjustmentCommands(InventoryTransaction transaction,
            List<OrderLumberLineItem> previousLineItems, List<OrderLumberLineItem> currentLineItems,
            int adjustmentMultiplier, bool applyInventoryQuantityChanges)
        {
            var commands = GetAddedLumberProductAddjustmentCommands(transaction,
                previousLineItems, currentLineItems, adjustmentMultiplier, applyInventoryQuantityChanges).ToList();

            commands.AddRange(GetEditedLumberProductAddjustmentCommands(transaction, previousLineItems, currentLineItems,
                adjustmentMultiplier, applyInventoryQuantityChanges));
            
            commands.AddRange(GetRemovedLumberProductAddjustmentCommands(transaction, previousLineItems, currentLineItems,
                adjustmentMultiplier, applyInventoryQuantityChanges));

            return commands;
        }

        private IEnumerable<DatabaseCommand> GetAddedLumberProductAddjustmentCommands(InventoryTransaction transaction,
            IEnumerable<OrderLumberLineItem> previousLineItems, IEnumerable<OrderLumberLineItem> currentLineItems,
            int adjustmentMultiplier, bool applyInventoryQuantityChanges)
        {
            return
                currentLineItems.Where(
                    i => !previousLineItems.Any(p => p.LumberProductId == i.LumberProductId && p.OrderId == i.OrderId))
                    .Select(
                        i =>
                            new AdjustLumberProductQuantity(transaction, i.LumberProductId,
                                i.Quantity*adjustmentMultiplier, applyInventoryQuantityChanges));
        }

        private IEnumerable<DatabaseCommand> GetEditedLumberProductAddjustmentCommands(InventoryTransaction transaction,
            IEnumerable<OrderLumberLineItem> previousLineItems, IEnumerable<OrderLumberLineItem> currentLineItems,
            int adjustmentMultiplier, bool applyInventoryQuantityChanges)
        {
            if (currentLineItems == null) return null;

            return from currentLineItem in currentLineItems.Where(
                c =>
                    previousLineItems.Any(
                        p =>
                            p.LumberProductId == c.LumberProductId &&
                            p.OrderId == c.OrderId && p.Quantity != c.Quantity))
                let previousLineItem = previousLineItems.Single(
                    p =>
                        p.LumberProductId == currentLineItem.LumberProductId &&
                        p.OrderId == currentLineItem.OrderId && p.Quantity != currentLineItem.Quantity)
                let delta = (currentLineItem.Quantity - previousLineItem.Quantity)*adjustmentMultiplier
                select
                    new AdjustLumberProductQuantity(transaction, currentLineItem.LumberProductId, delta,
                        applyInventoryQuantityChanges);
        }

        private IEnumerable<DatabaseCommand> GetRemovedLumberProductAddjustmentCommands(InventoryTransaction transaction,
            IEnumerable<OrderLumberLineItem> previousLineItems, IEnumerable<OrderLumberLineItem> currentLineItems,
            int adjustmentMultiplier, bool applyInventoryQuantityChanges)
        {
            return
                previousLineItems.Where(
                    i => !currentLineItems.Any(p => p.LumberProductId == i.LumberProductId && p.OrderId == i.OrderId))
                    .Select(
                        i =>
                            new AdjustLumberProductQuantity(transaction, i.LumberProductId,
                                -i.Quantity*adjustmentMultiplier, applyInventoryQuantityChanges));
        }

        private List<DatabaseCommand> GetMiscellaneousProductQuantityAdjustmentCommands(InventoryTransaction transaction,
            List<OrderMiscellaneousLineItem> previousLineItems, List<OrderMiscellaneousLineItem> currentLineItems,
            int adjustmentMultiplier, bool applyInventoryQuantityChanges)
        {
            var commands = GetAddedMiscellaneousProductAddjustmentCommands(transaction, previousLineItems, currentLineItems,
                adjustmentMultiplier, applyInventoryQuantityChanges).ToList();

            commands.AddRange(GetEditedMiscellaneousProductAddjustmentCommands(transaction, previousLineItems, currentLineItems,
                adjustmentMultiplier, applyInventoryQuantityChanges));

            commands.AddRange(GetRemovedMiscellaneousProductAddjustmentCommands(transaction, previousLineItems, currentLineItems,
                adjustmentMultiplier, applyInventoryQuantityChanges));

            return commands;
        }

        private IEnumerable<DatabaseCommand> GetAddedMiscellaneousProductAddjustmentCommands(InventoryTransaction transaction,
            IEnumerable<OrderMiscellaneousLineItem> previousLineItems, IEnumerable<OrderMiscellaneousLineItem> currentLineItems,
            int adjustmentMultiplier, bool applyInventoryQuantityChanges)
        {
            return currentLineItems.Where(
                    i => !previousLineItems.Any(p => p.MiscellaneousProductId == i.MiscellaneousProductId && p.OrderId == i.OrderId))
                    .Select(
                        i =>
                            new AdjustMiscellaneousProductQuantity(transaction, i.MiscellaneousProductId,
                                i.Quantity * adjustmentMultiplier, applyInventoryQuantityChanges));
        }

        private IEnumerable<DatabaseCommand> GetEditedMiscellaneousProductAddjustmentCommands(InventoryTransaction transaction,
            IEnumerable<OrderMiscellaneousLineItem> previousLineItems, List<OrderMiscellaneousLineItem> currentLineItems,
            int adjustmentMultiplier, bool applyInventoryQuantityChanges)
        {
            if (currentLineItems == null) return null;

            return from currentLineItem in currentLineItems.Where(
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
                        applyInventoryQuantityChanges);
        }

        private IEnumerable<DatabaseCommand> GetRemovedMiscellaneousProductAddjustmentCommands(InventoryTransaction transaction,
            IEnumerable<OrderMiscellaneousLineItem> previousLineItems, IEnumerable<OrderMiscellaneousLineItem> currentLineItems,
            int adjustmentMultiplier, bool applyInventoryQuantityChanges)
        {
            return previousLineItems.Where(
                    i => !currentLineItems.Any(p => p.MiscellaneousProductId == i.MiscellaneousProductId && p.OrderId == i.OrderId))
                    .Select(
                        i =>
                            new AdjustMiscellaneousProductQuantity(transaction, i.MiscellaneousProductId,
                                -i.Quantity * adjustmentMultiplier, applyInventoryQuantityChanges));
        }
    }
}
