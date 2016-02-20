using System.Collections.Generic;
using System.Linq;
using Thermory.Data.Commands;
using Thermory.Data.Models;
using Thermory.Domain;

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

        public void UpdateProductInventory(int userId, ILumberProduct[] lumberProducts,
            IMiscellaneousProduct[] miscProducts)
        {
            var commands = new List<DatabaseCommand>();

            var inventoryTransaction = new InventoryTransaction {UserId = userId};
            var createInventoryTransactionCommand = new CreateInventoryTransaction(inventoryTransaction);
            commands.Add(createInventoryTransactionCommand);

            var lumberUpdateCommands = lumberProducts.Select(lp => new UpdateLumberProductInventory(lp)).ToList();
            commands.AddRange(lumberUpdateCommands);

            var miscUpdateCommands = miscProducts.Select(mp => new UpdateMiscellaneousProductInventory(mp)).ToList();
            commands.AddRange(miscUpdateCommands);

            var transaction = new TransactionalCommand(commands);
            transaction.Execute();
        }
    }
}
