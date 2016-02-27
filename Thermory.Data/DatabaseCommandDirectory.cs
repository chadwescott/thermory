using System;
using System.Collections.Generic;
using System.Linq;
using Thermory.Data.CommandBuilders;
using Thermory.Data.Commands;
using Thermory.Data.Models;
using Thermory.Domain;
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

        //public void CreateOrder(int userId, IOrder order, ILumberProduct[] lumberProducts,
        //    IMiscellaneousProduct[] miscProducts)
        //{
        //    var order = new Order();
        //    var builder = new CreateOrderBuilder(userId, lumberProducts, miscProducts, order);
        //    var commands = builder.Commands;
        //    var transaction = new TransactionalCommand(commands);
        //    transaction.Execute();
        //}

        public IList<IDbLumberCategory> GetAllLumberCategories()
        {
            var command = new GetAllLumberCategories();
            command.Execute();
            return command.Result;
        }

        public IList<IDbLumberProduct> GetAllLumberProducts()
        {
            var command = new GetAllLumberProducts();
            command.Execute();
            return command.Result;
        }

        public IList<IDbMiscellaneousCategory> GetAllMiscellaneousCategories()
        {
            var command = new GetAllMiscellaneousCategories();
            command.Execute();
            return command.Result;
        }

        public IList<IDbMiscellaneousProduct> GetAllMiscellaneousProducts()
        {
            var command = new GetAllMiscellaneousProducts();
            command.Execute();
            return command.Result;
        }

        internal Guid GetTransactionTypeIdByEnum(TransactionTypes transactionType)
        {
            var command = new GetAllTransactionTypes();
            command.Execute();
            var transactionTypes = command.Result;

            return transactionTypes.Single(t => t.Name == transactionType.ToString()).Id;
        }

        public void UpdateProductInventory(int userId, TransactionTypes transactionType,
            ILumberProduct[] lumberProducts, IMiscellaneousProduct[] miscProducts)
        {
            var transactionTypeId = GetTransactionTypeIdByEnum(transactionType);
            var builder = new InventoryTransactionBuilder(userId, transactionTypeId, lumberProducts, miscProducts);
            var commands = builder.Commands;
            var transaction = new TransactionalCommand(commands);
            transaction.Execute();
        }
    }
}
