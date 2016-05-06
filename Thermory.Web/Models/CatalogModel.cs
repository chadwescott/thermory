using System;
using System.Linq;

namespace Thermory.Web.Models
{
    public class CatalogModel : InventoryModel
    {
        public string SelectedCategoryName
        {
            get
            {
                if (SelectedCategoryId == Guid.Empty)
                    return "Add a Category";
                if (LumberCategories.Any(c => c.Id == SelectedCategoryId))
                    return LumberCategories.SingleOrDefault(c => c.Id == SelectedCategoryId).Name;
                if (MiscellaneousCategories.Any(c => c.Id == SelectedCategoryId))
                    return MiscellaneousCategories.SingleOrDefault(c => c.Id == SelectedCategoryId).Name;
                return string.Empty;
            }
        }

        public Guid? SelectedCategoryId { get; set; }
    }
}