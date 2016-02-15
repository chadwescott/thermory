using System.Collections.Generic;
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

        public IList<IDbProductFamily> GetAllProductFamilies()
        {
            var command = new GetAllProductFamilies();
            command.Execute();
            return command.Result;
        }

        public IList<IDbProductInventory> GetAllLumberProductInventories()
        {
            var command = new GetAllLumberProductInventories();
            command.Execute();
            return command.Result;
        }

        public IList<IDbLumberFamily> GetAllLumberFamilies()
        {
            var command = new GetAllLumberFamilies();
            command.Execute();
            return command.Result;
        }

        public IList<IDbLumberProduct> GetAllLumberProducts()
        {
            var command = new GetAllLumberProducts();
            command.Execute();
            return command.Result;
        }

        public IList<IDbMiscellaneousProduct> GetAllMiscellaneousProducts()
        {
            var command = new GetAllMiscellaneousProducts();
            command.Execute();
            return command.Result;
        }

        public void UpdateProductInventory<T>(IInventory<T>[] inventory) where T : IProduct
        {
            var command = new UpdateProductInventory<T>(inventory);
            var transaction = new TransactionalCommand(new[] { command });
            transaction.Execute();
        }
    }
}
