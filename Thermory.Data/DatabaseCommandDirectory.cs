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

        public void CreateOrder(int userId, Order order, OrderLumberLineItem[] lumberLineItems, OrderMiscellaneousLineItem[] miscLineItems)
        {
            ExecuteBuilderCommand(new CreateOrderBuilder(userId, order, lumberLineItems, miscLineItems));
        }

        public void DeleteOrder(int userId, Order order)
        {
            ExecuteBuilderCommand(new DeleteOrderBuilder(userId, order));
        }

        public void EditOrder(int userId, Order order, OrderLumberLineItem[] lumberLineItems, OrderMiscellaneousLineItem[] miscLineItems)
        {
            ExecuteBuilderCommand(new EditOrderBuilder(userId, order, lumberLineItems, miscLineItems));
        }

        public IList<Customer> GetAllCustomers()
        {
            return ExecuteCommand(new GetAllCustomers());
        }

        public IList<LumberCategory> GetAllLumberCategories()
        {
            return ExecuteCommand(new GetAllLumberCategories());
        }

        public IList<LumberProduct> GetAllLumberProducts()
        {
            return ExecuteCommand(new GetAllLumberProducts());
        }

        public IList<MiscellaneousCategory> GetAllMiscellaneousCategories()
        {
            return ExecuteCommand(new GetAllMiscellaneousCategories());
        }

        public IList<MiscellaneousProduct> GetAllMiscellaneousProducts()
        {
            return ExecuteCommand(new GetAllMiscellaneousProducts());
        }

        public IList<OrderStatus> GetAllOrderStatuses()
        {
            return ExecuteCommand(new GetAllOrderStatuses());
        }

        public IList<OrderType> GetAllOrderTypes()
        {
            return ExecuteCommand(new GetAllOrderTypes());
        }

        public IList<PackagingType> GetAllPackagingTypes()
        {
            return ExecuteCommand(new GetAllPackagingTypes());
        }

        public IList<InventoryTransaction> GetInventoryTransactionsByOrderId(Guid orderId)
        {
            return ExecuteCommand(new GetInventoryTransactionsByOrderId(orderId));
        }

        public IList<InventoryTransaction> GetLumberTypeHistory(Guid lumberTypeId)
        {
            return ExecuteCommand(new GetLumberTypeHistory(lumberTypeId));
        } 

        public Order GetOrderById(Guid id)
        {
            return ExecuteCommand(new GetOrderById(id));
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
            return ExecuteCommand(new GetOrderStatusSummary(type));
        } 

        public Package GetPackageById(Guid id)
        {
            return ExecuteCommand(new GetPackageById(id));
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
            return ExecuteCommand(new GetUserRolesByUserId(userId));
        }

        public void InventoryAudit(int userId, TransactionTypes transactionType,
            LumberProduct[] lumberProducts, MiscellaneousProduct[] miscProducts)
        {
            var transactionTypeId = GetTransactionTypeIdByEnum(transactionType);
            ExecuteBuilderCommand(new InventoryAuditBuilder(userId, transactionTypeId, lumberProducts, miscProducts));
        }

        public void LoadOrder(int userId, Guid orderId, int minutesTaken)
        {
            ExecuteBuilderCommand(new LoadOrderBuilder(userId, orderId, minutesTaken));
        }

        public void PullOrder(int userId, Guid orderId, int minutesTaken)
        {
            ExecuteBuilderCommand(new PullOrderBuilder(userId, orderId, minutesTaken));
        }

        public void ReceiveOrder(int userId, Order order)
        {
            ExecuteBuilderCommand(new ReceiveOrderBuilder(userId, order));
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
            ExecuteBuilderCommand(new SavePackagesBuilder(userId, orderId, lumberLineItems, miscLineItems));
        }

        public void UpdatePackages(Package[] packages)
        {
            var transaction = new TransactionalCommand(packages.Select(p => new UpdatePackage(p)));
            transaction.Execute();
        }

        public void UpdateUserRoles(UserProfile user)
        {
            ExecuteBuilderCommand(new UpdateUserRolesBuilder(user));
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

        private static void ExecuteBuilderCommand(CommandBuilder builder)
        {
            var transaction = new TransactionalCommand(builder.Commands);
            transaction.Execute();
        }
    }
}
