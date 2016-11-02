using System.Collections.Generic;
using Thermory.Domain.Models;

namespace Thermory.Web.Models
{
    public class LumberSubCategoryViewModel : CatalogFormViewModel
    {
        public LumberCategory Category { get; set; }

        public List<LumberCategory> Categories { get; set; } 

        public LumberSubCategory SubCategory { get; set; }
    }
}