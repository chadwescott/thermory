using System;
using System.Collections.Generic;
using Thermory.Data.Commands;
using Thermory.Data.Extensions;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;

namespace Thermory.Data.CommandBuilders
{
    internal abstract class OrderBuilder : CommandBuilder
    {
        public Order Order { get; protected set; }

        protected Order GetOrder(Guid orderId)
        {
            var getOrderCommand = new GetOrderById(orderId);
            getOrderCommand.Execute();
            return getOrderCommand.Result;
        }

        protected void AddCreatedLumberLineItemCommands(Order order, IEnumerable<OrderLumberLineItem> updatedLumberLineItems)
        {
            var createOrderLumberLinesCommands = updatedLumberLineItems.MakeCreateOrderLumberLineItemCommands(order);
            Commands.AddRange(createOrderLumberLinesCommands);
        }

        protected void AddCreateMiscellaneousLineItemCommands(Order order, IEnumerable<OrderMiscellaneousLineItem> updatedMiscellaneousLineItems)
        {
            var createOrderMiscellaneousLinesCommands = updatedMiscellaneousLineItems.MakeCreateOrderMiscellaneousLineItemCommands(order);
            Commands.AddRange(createOrderMiscellaneousLinesCommands);
        }

        protected InventoryTransaction CreateInventoryTransaction(int userId, Order order)
        {
            var transactionTypeId =
                DatabaseCommandDirectory.Instance.GetTransactionTypeIdByEnum(TransactionType);
            var transaction = new InventoryTransaction
            {
                UserId = userId,
                Order = order,
                OrderId = order.Id,
                TransactionTypeId = transactionTypeId
            };
            CreateInventoryTransaction(transaction);
            return transaction;
        }

        protected InventoryTransaction CreateInventoryTransaction(int userId, Guid orderId)
        {
            var transactionTypeId =
                DatabaseCommandDirectory.Instance.GetTransactionTypeIdByEnum(TransactionType);
            var transaction = new InventoryTransaction
            {
                UserId = userId,
                OrderId = orderId,
                TransactionTypeId = transactionTypeId
            };
            CreateInventoryTransaction(transaction);
            return transaction;
        }

        private void CreateInventoryTransaction(InventoryTransaction transaction)
        {
            var createInventoryTransactionCommand = new CreateInventoryTransaction(transaction);
            Commands.Add(createInventoryTransactionCommand);
        }

        protected abstract TransactionTypes TransactionType { get; }
    }
}
