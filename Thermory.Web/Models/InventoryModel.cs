using System.Collections.Generic;
using Thermory.Domain.Models;

namespace Thermory.Web.Models
{
    public class InventoryModel
    {
        public IList<LumberCategory> LumberCategories { get; set; }

        public IList<MiscellaneousCategory> MiscellaneousCategories { get; set; }
    }
}