using System.Collections.Generic;
using System.Linq;
using Thermory.Data;
using Thermory.Domain.Commands;
using Thermory.Domain.Models;

namespace Thermory.Business.Commands
{
    internal class RefreshLumberProductQuantities : ICommand
    {
        private readonly IList<LumberCategory> _lumberCategories;

        public RefreshLumberProductQuantities(IList<LumberCategory> lumberCategories)
        {
            _lumberCategories = lumberCategories;
        }

        public void Execute()
        {
            var inventory = DatabaseCommandDirectory.Instance.GetAllLumberProducts();
            foreach (var category in _lumberCategories)
            {
                foreach (var subcategory in category.LumberSubCategories)
                {
                    foreach (var lumberType in subcategory.LumberTypes)
                    {
                        foreach (var lumberProduct in lumberType.LumberProducts)
                        {
                            var lumberInventory =
                                inventory.SingleOrDefault(
                                    i => i.Id == lumberProduct.Id && i.Quantity != lumberProduct.Quantity);
                            if (lumberInventory == null) continue;
                            lumberProduct.Quantity = lumberInventory.Quantity;
                        }
                    }
                }
            }
        }
    }
}
