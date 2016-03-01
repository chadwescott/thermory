using System.Collections.Generic;
using Thermory.Domain;

namespace Thermory.Web.ViewModels
{
    public class InventoryViewModel
    {
        public IList<ILumberCategory> LumberCategories { get; set; }

        public IList<IMiscellaneousCategory> MiscellaneousCategories { get; set; }
    }
}