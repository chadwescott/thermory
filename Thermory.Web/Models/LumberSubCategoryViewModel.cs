using Thermory.Domain.Models;

namespace Thermory.Web.Models
{
    public class LumberSubCategoryViewModel : CatalogFormViewModel
    {
        public LumberSubCategory SubCategory { get; set; }

        public int MaxSortOrder { get; set; }
    }
}