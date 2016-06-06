using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly object _lumberCategoryLock = new object();
        private readonly object _miscCategoryLock = new object();

        public static CommandDirectory Instance
        {
            get { return _instance ?? (_instance = new CommandDirectory()); }
        }

        private CommandDirectory()
        {
            LoadCatalog();
        }

        private void LoadCatalog()
        {
            LoadLumberCategories();
            LoadMiscellaneousCategories();
        }

        private void LoadLumberCategories()
        {
            lock (_lumberCategoryLock)
            {
                var command = new GetAllLumberCategories();
                command.Execute();
                _lumberCategories = command.Result;
            }
        }

        private void LoadMiscellaneousCategories()
        {
            lock (_miscCategoryLock)
            {
                var command = new GetAllMiscellaneousCategories();
                command.Execute();
                _miscellaneousCategories = command.Result;
            }
        }

        public void DeleteOrder(int userId, Order order)
        {
            DatabaseCommandDirectory.Instance.DeleteOrder(userId, order);
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

        public IList<PackagingType> GetAllPackagingTypes()
        {
            return DatabaseCommandDirectory.Instance.GetAllPackagingTypes();
        }

        public IList<InventoryTransaction> GetInventoryTransactionsByOrderId(Guid orderId)
        {
            return DatabaseCommandDirectory.Instance.GetInventoryTransactionsByOrderId(orderId);
        }

        public Order GetOrderById(Guid id)
        {
            return DatabaseCommandDirectory.Instance.GetOrderById(id);
        }

        public OrderStatus GetOrderStatusByOrderStatusEnum(OrderStatuses orderStatus)
        {
            return DatabaseCommandDirectory.Instance.GetAllOrderStatuses().SingleOrDefault(s => s.OrderStatusEnum == orderStatus);
        }

        public IList<OrderStatus> GetOrderStatusesByOrderTypeId(Guid orderTypeId)
        {
            return DatabaseCommandDirectory.Instance.GetAllOrderStatuses().Where(s => s.OrderTypeId == orderTypeId).OrderBy(s => s.SortOrder).ToList();
        }

        public OrderType GetOrderTypeByOrderTypeEnum(OrderTypes orderType)
        {
            return DatabaseCommandDirectory.Instance.GetAllOrderTypes().SingleOrDefault(t => t.OrderTypeEnum == orderType);
        }

        public IList<UserRoleXref> GetUserRolesByUserId(int userId)
        {
            return DatabaseCommandDirectory.Instance.GetUserRolesByUserId(userId);
        }

        public void LoadOrder(int userId, Order order)
        {
            var minutesTaken = order.MinutesToLoad == null ? 0 : order.MinutesToLoad.Value;
            DatabaseCommandDirectory.Instance.LoadOrder(userId, order.Id, minutesTaken);
        }

        public void PullOrder(int userId, Order order)
        {
            var minutesTaken = order.MinutesToPull == null ? 0 : order.MinutesToPull.Value;
            DatabaseCommandDirectory.Instance.PullOrder(userId, order.Id, minutesTaken);
        }

        public void ReceiveOrder(int userId, Order order)
        {
            DatabaseCommandDirectory.Instance.ReceiveOrder(userId, order);
        }

        public void SaveLumberCategory(LumberCategory model)
        {
            DatabaseCommandDirectory.Instance.SaveLumberCategory(model);
            new TaskFactory().StartNew(LoadLumberCategories);
        }

        public void SaveLumberProduct(LumberProduct model)
        {
            DatabaseCommandDirectory.Instance.SaveLumberProduct(model);
            new TaskFactory().StartNew(LoadLumberCategories);
        }

        public void SaveLumberSubCategory(LumberSubCategory model)
        {
            DatabaseCommandDirectory.Instance.SaveLumberSubCategory(model);
            new TaskFactory().StartNew(LoadLumberCategories);
        }

        public void SaveLumberType(LumberType model)
        {
            DatabaseCommandDirectory.Instance.SaveLumberType(model);
            new TaskFactory().StartNew(LoadLumberCategories);
        }

        public void SaveMiscellaneousCategory(MiscellaneousCategory model)
        {
            DatabaseCommandDirectory.Instance.SaveMiscellaneousCategory(model);
            new TaskFactory().StartNew(LoadMiscellaneousCategories);
        }

        public void SaveMiscellaneousProduct(MiscellaneousProduct model)
        {
            DatabaseCommandDirectory.Instance.SaveMiscellaneousProduct(model);
            new TaskFactory().StartNew(LoadMiscellaneousCategories);
        }

        public void SaveMiscellaneousSubCategory(MiscellaneousSubCategory model)
        {
            DatabaseCommandDirectory.Instance.SaveMiscellaneousSubCategory(model);
            new TaskFactory().StartNew(LoadMiscellaneousCategories);
        }

        public void SaveOrder(int userId, Order order, OrderLumberLineItem[] lumberLineItems,
            OrderMiscellaneousLineItem[] miscLineItems)
        {
            if (order.Id == Guid.Empty)
                DatabaseCommandDirectory.Instance.CreateOrder(userId, order, lumberLineItems, miscLineItems);
            else
                DatabaseCommandDirectory.Instance.EditOrder(userId, order, lumberLineItems, miscLineItems);
        }

        public void UpdateProductInventory(int userId, TransactionTypes transactionType, LumberProduct[] lumberProducts,
            MiscellaneousProduct[] miscProducts)
        {
            DatabaseCommandDirectory.Instance.InventoryAudit(userId, transactionType, lumberProducts,
                miscProducts);
        }

        public void UpdateUserRoles(UserProfile user)
        {
            DatabaseCommandDirectory.Instance.UpdateUserRoles(user);
        }

        public void WarehouseReceivedOrder(int userId, Order order)
        {
            DatabaseCommandDirectory.Instance.WarehouseReceivedOrder(userId, order);
        }
    }
}
