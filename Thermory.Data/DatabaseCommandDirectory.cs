﻿using System;
using System.Collections.Generic;
using System.Linq;
using Thermory.Data.CommandBuilders;
using Thermory.Data.Commands;
using Thermory.Domain.Models;
using Thermory.Domain.Enums;

namespace Thermory.Data
{
    public class DatabaseCommandDirectory
    {
        private static DatabaseCommandDirectory _instance;

        public static DatabaseCommandDirectory Instance
        {
            get { return _instance ?? (_instance = new DatabaseCommandDirectory()); }
        }

        private DatabaseCommandDirectory()
        { }

        public void CreateOrder(int userId, OrderTypes orderType, OrderLumberLineItem[] lumberLineItems,
            OrderMiscellaneousLineItem[] miscLineItems)
        {
            var builder = new CreateOrderBuilder(userId, orderType, lumberLineItems, miscLineItems);
            var commands = builder.Commands;
            var transaction = new TransactionalCommand(commands);
            transaction.Execute();
        }

        public IList<LumberCategory> GetAllLumberCategories()
        {
            var command = new GetAllLumberCategories();
            command.Execute();
            return command.Result;
        }

        public IList<LumberProduct> GetAllLumberProducts()
        {
            var command = new GetAllLumberProducts();
            command.Execute();
            return command.Result;
        }

        public IList<MiscellaneousCategory> GetAllMiscellaneousCategories()
        {
            var command = new GetAllMiscellaneousCategories();
            command.Execute();
            return command.Result;
        }

        public IList<MiscellaneousProduct> GetAllMiscellaneousProducts()
        {
            var command = new GetAllMiscellaneousProducts();
            command.Execute();
            return command.Result;
        }

        public Order GetOrderById(Guid id)
        {
            var command = new GetOrderById(id);
            command.Execute();
            return command.Result;
        }

        internal Guid GetOrderTypeyEnum(OrderTypes orderType)
        {
            var command = new GetAllOrderTypes();
            command.Execute();
            var orderTypes = command.Result;

            return orderTypes.Single(t => t.Name == orderType.ToString()).Id;
        }

        internal Guid GetTransactionTypeyEnum(TransactionTypes transactionType)
        {
            var command = new GetAllTransactionTypes();
            command.Execute();
            var transactionTypes = command.Result;

            return transactionTypes.Single(t => t.Name == transactionType.ToString()).Id;
        }

        public void InventoryAudit(int userId, TransactionTypes transactionType,
            LumberProduct[] lumberProducts, MiscellaneousProduct[] miscProducts)
        {
            var transactionTypeId = GetTransactionTypeyEnum(transactionType);
            var builder = new InventoryAuditBuilder(userId, transactionTypeId, lumberProducts, miscProducts);
            var commands = builder.Commands;
            var transaction = new TransactionalCommand(commands);
            transaction.Execute();
        }
    }
}
