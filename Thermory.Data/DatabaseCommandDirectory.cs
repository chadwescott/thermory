using System;
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

        public Order CreateOrder(int userId, OrderTypes orderType, Customer customer, PackagingType packagingType,
            OrderLumberLineItem[] lumberLineItems, OrderMiscellaneousLineItem[] miscLineItems)
        {
            var builder = new CreateOrderBuilder(userId, orderType, customer, packagingType, lumberLineItems,
                miscLineItems);
            var transaction = new TransactionalCommand(builder.Commands);
            transaction.Execute();
            return builder.Order;
        }

        public Order DeleteOrder(int userId, Guid orderId)
        {
            var builder = new DeleteOrderBuilder(userId, orderId);
            var transaction = new TransactionalCommand(builder.Commands);
            transaction.Execute();
            return builder.Order;
        }

        public Order EditOrder(int userId, Guid orderId, Customer customer, PackagingType packagingType,
            OrderLumberLineItem[] lumberLineItems, OrderMiscellaneousLineItem[] miscLineItems)
        {
            var builder = new EditOrderBuilder(userId, orderId, customer, packagingType, lumberLineItems, miscLineItems);
            var transaction = new TransactionalCommand(builder.Commands);
            transaction.Execute();
            return builder.Order;
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

        private IList<OrderType> _orderTypes;

        internal Guid GetOrderTypeIdByEnum(OrderTypes orderType)
        {
            if (_orderTypes == null)
                _orderTypes = ExecuteCommand(new GetAllOrderTypes());
            return _orderTypes.Single(t => t.OrderTypeEnum == orderType).Id;
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

        public void UpdateUserRoles(UserProfile user)
        {
            var builder = new UpdateUserRolesBuilder(user);
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
