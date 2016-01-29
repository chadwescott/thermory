using System.Collections.Generic;
using System.Linq;
using Thermory.Business.Models;
using Thermory.Data;
using Thermory.Data.Commands;
using Thermory.Domain;

namespace Thermory.Business.Commands
{
    internal class UpdateLumberProductInventory : ICommand
    {
        private readonly IList<IProductCategory<ILumberSubCategory>> _lumberCategories;

        public UpdateLumberProductInventory(IList<IProductCategory<ILumberSubCategory>> lumberCategories)
        {
            _lumberCategories = lumberCategories;
        }

        public void Execute()
        {
            var inventory = DatabaseCommandDirectory.Instance.GetAllLumberProductInventories();
            foreach (var category in _lumberCategories)
            {
                foreach (var subcategory in category.ProductSubCategories)
                {
                    foreach (var productType in subcategory.ProductTypes)
                    {
                        foreach (var product in productType.Products)
                        {
                            var productInventory = inventory.SingleOrDefault(i => i.Id == product.Id);
                            if (productInventory == null) continue;
                            product.Inventory = new LumberInventory{Product = product, Quantity = productInventory.Quantity};
                        }
                    }
                }
            }
        }
    }
}
