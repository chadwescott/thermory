using System.Collections.Generic;
using Thermory.Data.Commands;
using Thermory.Data.Models;

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

        public IList<IDbProductInventory> GetAllProductInventories()
        {
            var command = new GetAllProductInventories();
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
    }
}
