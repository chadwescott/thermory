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

        public IList<IDbMiscellaneousProduct> GetAllMiscellaneousProducts()
        {
            var command = new GetAllMiscellaneousProducts();
            command.Execute();
            return command.Result;
        }

        public void UpdateLumberProductInventory(ILumberProduct[] lumberProducts)
        {
            var updateCommands = lumberProducts.Select(lp => new UpdateLumberProductInventory(lp)).ToArray();
            var transaction = new TransactionalCommand(updateCommands);
            transaction.Execute();
        }
    }
}
