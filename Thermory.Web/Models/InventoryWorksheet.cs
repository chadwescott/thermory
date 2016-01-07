using System.Collections.Generic;
using Thermory.Domain;

namespace Thermory.Web.Models
{
    public class InventoryWorksheet
    {
        public IList<IProductCategory<ILumberSubCategory>> LumberProductCategories { get; set; }
    }
}