using System.Collections.Generic;
using Thermory.Domain;

namespace Thermory.Web.Models
{
    public class InventoryWorksheet
    {
        public IList<ILumberCategory> LumberCategories { get; set; }

        public IList<IProductCategory<IProductSubCategory>> MiscellaneousCategories { get; set; }
    }
}