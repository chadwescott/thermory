using System.Collections.Generic;
using Thermory.Domain;
using Thermory.Domain.Enums;

namespace Thermory.Web.ViewModels
{
    public class OrderFormViewModel
    {
        public OrderTypes OrderType { get; set; }

        public IList<ILumberCategory> LumberCategories { get; set; }

        public IList<IMiscellaneousCategory> MiscellaneousCategories { get; set; }
    }
}