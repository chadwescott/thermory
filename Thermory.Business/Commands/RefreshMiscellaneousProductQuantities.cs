using System.Collections.Generic;
using System.Linq;
using Thermory.Data;
using Thermory.Data.Commands;
using Thermory.Domain;

namespace Thermory.Business.Commands
{
    internal class RefreshMiscellaneousProductQuantities : ICommand
    {
        private readonly IList<IMiscellaneousCategory> _miscellaneousCategories;

        public RefreshMiscellaneousProductQuantities(IList<IMiscellaneousCategory> miscellaneousCategories)
        {
            _miscellaneousCategories = miscellaneousCategories;
        }

        public void Execute()
        {
            var inventory = DatabaseCommandDirectory.Instance.GetAllMiscellaneousProducts();
            foreach (var category in _miscellaneousCategories)
            {
                foreach (var subcategory in category.MiscellaneousSubCategories)
                {
                    foreach (var miscellaneousProduct in subcategory.MiscellaneousProducts)
                    {
                        var miscellaneousInventory =
                            inventory.SingleOrDefault(
                                i => i.Id == miscellaneousProduct.Id && i.Quantity != miscellaneousProduct.Quantity);
                        if (miscellaneousInventory == null) continue;
                        miscellaneousProduct.Quantity = miscellaneousInventory.Quantity;
                    }
                }
            }
        }
    }
}
