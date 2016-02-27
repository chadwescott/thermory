using System.Collections.Generic;
using Thermory.Business.Commands;
using Thermory.Data;
using Thermory.Domain;
using Thermory.Domain.Enums;

namespace Thermory.Business
{
    public class CommandDirectory
    {
        private static CommandDirectory _instance;
        private static IList<ILumberCategory> _lumberCategories;
        private static IList<IMiscellaneousCategory> _miscellaneousCategories;
        private readonly object _categoryLock = new object();

        public static CommandDirectory Instance
        {
            get { return _instance ?? (_instance = new CommandDirectory()); }
        }

        private CommandDirectory()
        {
            lock (_categoryLock)
            {
                LoadLumberCategories();
                LoadMiscellaneousCategories();
            }
        }

        private static void LoadLumberCategories()
        {
            if (_lumberCategories != null) return;
            var command = new GetAllLumberCategories();
            command.Execute();
            _lumberCategories = command.Result;
        }

        private static void LoadMiscellaneousCategories()
        {
            if (_miscellaneousCategories != null) return;
            var command = new GetAllMiscellaneousCategories();
            command.Execute();
            _miscellaneousCategories = command.Result;
        }

        public IList<ILumberCategory> GetAllLumberCategories()
        {
            var command = new RefreshLumberProductQuantities(_lumberCategories);
            command.Execute();
            return _lumberCategories;
        }

        public IList<IMiscellaneousCategory> GetAllMiscellaneousCategories()
        {
            var command = new RefreshMiscellaneousProductQuantities(_miscellaneousCategories);
            command.Execute();
            return _miscellaneousCategories;
        }

        public void UpdateProductInventory(int userId, TransactionTypes transactionType, ILumberProduct[] lumberProducts,
            IMiscellaneousProduct[] miscProducts)
        {
            DatabaseCommandDirectory.Instance.UpdateProductInventory(userId, transactionType, lumberProducts,
                miscProducts);
        }
    }
}
