﻿using System;
using System.Collections.Generic;
using Thermory.Business.Commands;
using Thermory.Data;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;

namespace Thermory.Business
{
    public class CommandDirectory
    {
        private static CommandDirectory _instance;
        private static IList<LumberCategory> _lumberCategories;
        private static IList<MiscellaneousCategory> _miscellaneousCategories;
        private readonly object _categoryLock = new object();

        public static CommandDirectory Instance
        {
            get { return _instance ?? (_instance = new CommandDirectory()); }
        }

        private CommandDirectory()
        {
            lock (_categoryLock)
            {
                LoadLumberCategories();
                LoadMiscellaneousCategories();
            }
        }

        private static void LoadLumberCategories()
        {
            if (_lumberCategories != null) return;
            var command = new GetAllLumberCategories();
            command.Execute();
            _lumberCategories = command.Result;
        }

        private static void LoadMiscellaneousCategories()
        {
            if (_miscellaneousCategories != null) return;
            var command = new GetAllMiscellaneousCategories();
            command.Execute();
            _miscellaneousCategories = command.Result;
        }

        public void DeleteOrder(int userId, Guid orderId)
        {
            DatabaseCommandDirectory.Instance.DeleteOrder(userId, orderId);
        }

        public void SaveOrder(int userId, Guid orderId, OrderTypes orderType, Customer customer,
            OrderLumberLineItem[] lumberLineItems, OrderMiscellaneousLineItem[] miscLineItems)
        {
            if (orderId == Guid.Empty)
                DatabaseCommandDirectory.Instance.CreateOrder(userId, orderType, customer, lumberLineItems,
                    miscLineItems);
            else
                DatabaseCommandDirectory.Instance.EditOrder(userId, orderId, customer, lumberLineItems, miscLineItems);
        }

        public IList<Customer> GetAllCustomers()
        {
            return DatabaseCommandDirectory.Instance.GetAllCustomers();
        } 

        public IList<LumberCategory> GetAllLumberCategories()
        {
            var command = new RefreshLumberProductQuantities(_lumberCategories);
            command.Execute();
            return _lumberCategories;
        }

        public IList<MiscellaneousCategory> GetAllMiscellaneousCategories()
        {
            var command = new RefreshMiscellaneousProductQuantities(_miscellaneousCategories);
            command.Execute();
            return _miscellaneousCategories;
        }

        public IList<InventoryTransaction> GetInventoryTransactionsByOrderId(Guid orderId)
        {
            return DatabaseCommandDirectory.Instance.GetInventoryTransactionsByOrderId(orderId);
        }

        public Order GetOrderById(Guid id)
        {
            return DatabaseCommandDirectory.Instance.GetOrderById(id);
        }

        public void UpdateProductInventory(int userId, TransactionTypes transactionType, LumberProduct[] lumberProducts,
            MiscellaneousProduct[] miscProducts)
        {
            DatabaseCommandDirectory.Instance.InventoryAudit(userId, transactionType, lumberProducts,
                miscProducts);
        }
    }
}
