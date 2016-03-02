using System.Collections.Generic;
using Thermory.Domain;

namespace Thermory.Web.Models
{
    public class InventoryModel
    {
        public IList<ILumberCategory> LumberCategories { get; set; }

        public IList<IMiscellaneousCategory> MiscellaneousCategories { get; set; }
    }
}