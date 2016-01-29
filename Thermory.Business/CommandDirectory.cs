using System.Collections.Generic;
using Thermory.Business.Commands;
using Thermory.Data;
using Thermory.Data.Models;
using Thermory.Domain;

namespace Thermory.Business
{
    public class CommandDirectory
    {
        private static CommandDirectory _instance;
        private static IList<IProductCategory<ILumberSubCategory>> _lumberCategories;
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

        public IList<IDbProductFamily> GetAllProductFamilies()
        {
            return DatabaseCommandDirectory.Instance.GetAllProductFamilies();
        }

        public IList<IProductCategory<ILumberSubCategory>> GetAllLumberProducts()
        {
            return _lumberCategories;
        }

        public IList<IProductCategory<ILumberSubCategory>> GetAllLumberProductsWithInventory()
        {
            var command = new UpdateLumberProductInventory(_lumberCategories);
            command.Execute();
            return _lumberCategories;
        }
    }
}
