using System.Collections.Generic;
using Thermory.Business.Commands;
using Thermory.Data;
using Thermory.Domain;

namespace Thermory.Business
{
    public class CommandDirectory
    {
        private static CommandDirectory _instance;
        private static IList<ILumberCategory> _lumberCategories;
        private readonly object _categoryLock = new object();

        public static CommandDirectory Instance
        {
            get { return _instance ?? (_instance = new CommandDirectory()); }
        }

        private CommandDirectory()
        {
            lock (_categoryLock)
            {
                if (_lumberCategories != null) return;
                var command = new GetAllLumberProducts();
                command.Execute();
                _lumberCategories = command.Result;
            }
        }

        public IList<ILumberCategory> GetAllLumberProducts()
        {
            var command = new UpdateLumberProductInventory(_lumberCategories);
            command.Execute();
            return _lumberCategories;
        }

        public void UpdateLumberProductInventory(ILumberProduct[] lumberProducts)
        {
            DatabaseCommandDirectory.Instance.UpdateLumberProductInventory(lumberProducts);
        }
    }
}
