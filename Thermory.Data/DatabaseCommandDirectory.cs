﻿using System;
using System.Collections.Generic;
using System.Linq;
using Thermory.Data.CommandBuilders;
using Thermory.Data.Commands;
using Thermory.Domain.Commands;
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

        public void CreateOrder(int userId, Order order, OrderLumberLineItem[] lumberLineItems, OrderMiscellaneousLineItem[] miscLineItems)
        {
            var builder = new CreateOrderBuilder(userId, order,
                lumberLineItems, miscLineItems);
            var transaction = new TransactionalCommand(builder.Commands);
            transaction.Execute();
        }

        public void DeleteOrder(int userId, Order order)
        {
            var builder = new DeleteOrderBuilder(userId, order);
            var transaction = new TransactionalCommand(builder.Commands);
            transaction.Execute();
        }

        public void EditOrder(int userId, Order order, OrderLumberLineItem[] lumberLineItems, OrderMiscellaneousLineItem[] miscLineItems)
        {
            var builder = new EditOrderBuilder(userId, order, lumberLineItems, miscLineItems);
            var transaction = new TransactionalCommand(builder.Commands);
            transaction.Execute();
        }

        public IList<Customer> GetAllCustomers()
        {
            var command = new GetAllCustomers();
            command.Execute();
            return command.Result;
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

        public IList<OrderStatus> GetAllOrderStatuses()
        {
            var command = new GetAllOrderStatuses();
            command.Execute();
            return command.Result;
        }

        public IList<OrderType> GetAllOrderTypes()
        {
            var command = new GetAllOrderTypes();
            command.Execute();
            return command.Result;
        }

        public IList<PackagingType> GetAllPackagingTypes()
        {
            var command = new GetAllPackagingTypes();
            command.Execute();
            return command.Result;
        }

        public IList<InventoryTransaction> GetInventoryTransactionsByOrderId(Guid orderId)
        {
            var command = new GetInventoryTransactionsByOrderId(orderId);
            command.Execute();
            return command.Result;
        }

        public Order GetOrderById(Guid id)
        {
            var command = new GetOrderById(id);
            command.Execute();
            return command.Result;
        }

        public OrderStatus GetOrderStatusByEnum(OrderStatuses status, Guid? orderTypeId = null)
        {
            var statuses = GetAllOrderStatuses();
            return orderTypeId == null
                ? statuses.Single(s => s.OrderStatusEnum == status)
                : statuses.Single(s => s.OrderStatusEnum == status && s.OrderTypeId == orderTypeId);
        }

        public IList<OrderSummary> GetOrderStatusSummary(OrderTypes type)
        {
            var command = new GetOrderStatusSummary(type);
            command.Execute();
            return command.Result;
        } 

        public Package GetPackageById(Guid id)
        {
            var command = new GetPackageById(id);
            command.Execute();
            return command.Result;
        }

        private IList<TransactionType> _transactionTypes;
 
        internal Guid GetTransactionTypeIdByEnum(TransactionTypes transactionType)
        {
            if (_transactionTypes == null)
                _transactionTypes = ExecuteCommand(new GetAllTransactionTypes());
            return _transactionTypes.Single(t => t.Name == transactionType.ToString()).Id;
        }

        public IList<UserRoleXref> GetUserRolesByUserId(int userId)
        {
            var command = new GetUserRolesByUserId(userId);
            command.Execute();
            return command.Result;
        }

        public void InventoryAudit(int userId, TransactionTypes transactionType,
            LumberProduct[] lumberProducts, MiscellaneousProduct[] miscProducts)
        {
            var transactionTypeId = GetTransactionTypeIdByEnum(transactionType);
            var builder = new InventoryAuditBuilder(userId, transactionTypeId, lumberProducts, miscProducts);
            var transaction = new TransactionalCommand(builder.Commands);
            transaction.Execute();
        }

        public void LoadOrder(int userId, Guid orderId, int minutesTaken)
        {
            var builder = new LoadOrderBuilder(userId, orderId, minutesTaken);
            var transaction = new TransactionalCommand(builder.Commands);
            transaction.Execute();
        }

        public void PullOrder(int userId, Guid orderId, int minutesTaken)
        {
            var builder = new PullOrderBuilder(userId, orderId, minutesTaken);
            var transaction = new TransactionalCommand(builder.Commands);
            transaction.Execute();
        }

        public void ReceiveOrder(int userId, Order order)
        {
            var builder = new ReceiveOrderBuilder(userId, order);
            var transaction = new TransactionalCommand(builder.Commands);
            transaction.Execute();
        }

        public void SaveLumberCategory(LumberCategory model)
        {
            var command = new SaveLumberCategory(model);
            command.Execute();
        }

        public void SaveLumberProduct(LumberProduct model)
        {
            var command = new SaveLumberProduct(model);
            command.Execute();
        }

        public void SaveLumberSubCategory(LumberSubCategory model)
        {
            var command = new SaveLumberSubCategory(model);
            command.Execute();
        }

        public void SaveLumberType(LumberType model)
        {
            var command = new SaveLumberType(model);
            command.Execute();
        }

        public void SaveMiscellaneousCategory(MiscellaneousCategory model)
        {
            var command = new SaveMiscellaneousCategory(model);
            command.Execute();
        }

        public void SaveMiscellaneousProduct(MiscellaneousProduct model)
        {
            var command = new SaveMiscellaneousProduct(model);
            command.Execute();
        }

        public void SaveMiscellaneousSubCategory(MiscellaneousSubCategory model)
        {
            var command = new SaveMiscellaneousSubCategory(model);
            command.Execute();
        }

        public void SavePackages(int userId, Guid orderId, PackageLumberLineItem[] lumberLineItems, PackageMiscellaneousLineItem[] miscLineItems)
        {
            var builder = new SavePackagesBuilder(userId, orderId, lumberLineItems, miscLineItems);
            var transaction = new TransactionalCommand(builder.Commands);
            transaction.Execute();
        }

        public void UpdatePackages(Package[] packages)
        {
            var transaction = new TransactionalCommand(packages.Select(p => new UpdatePackage(p)));
            transaction.Execute();
        }

        public void UpdateUserRoles(UserProfile user)
        {
            var builder = new UpdateUserRolesBuilder(user);
            var transaction = new TransactionalCommand(builder.Commands);
            transaction.Execute();
        }

        public void WarehouseReceivedOrder(int userId, Order order)
        {
            var builder = new WarehouseReceviedOrderBuilder(userId, order);
            var transaction = new TransactionalCommand(builder.Commands);
            transaction.Execute();
        }

        private static T ExecuteCommand<T>(IGetCommand<T> command)
        {
            command.Execute();
            return command.Result;
        }
    }
}
