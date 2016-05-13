using System;
using System.Collections.Generic;
using System.Threading;
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
                if (_miscellaneousCategories != null) return;
                var command = new GetAllMiscellaneousCategories();
                command.Execute();
                _miscellaneousCategories = command.Result;
            }
        }

        public void DeleteOrder(int userId, Guid orderId)
        {
            DatabaseCommandDirectory.Instance.DeleteOrder(userId, orderId);
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

        public IList<UserRoleXref> GetUserRolesByUserId(int userId)
        {
            return DatabaseCommandDirectory.Instance.GetUserRolesByUserId(userId);
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

        public void SaveOrder(int userId, Guid orderId, OrderTypes orderType, Customer customer,
            PackagingType packagingType, OrderLumberLineItem[] lumberLineItems,
            OrderMiscellaneousLineItem[] miscLineItems)
        {
            if (orderId == Guid.Empty)
                DatabaseCommandDirectory.Instance.CreateOrder(userId, orderType, customer, packagingType,
                    lumberLineItems, miscLineItems);
            else
                DatabaseCommandDirectory.Instance.EditOrder(userId, orderId, customer, packagingType, lumberLineItems,
                    miscLineItems);
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
    }
}
