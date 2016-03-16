using System.Collections.Generic;
using Thermory.Domain.Enums;
using Thermory.Domain.Models;

namespace Thermory.Web.Models
{
    public class OrderFormModel
    {
        public Order Order { get; set; }

        public OrderTypes OrderType { get; set; }

        public List<ProductOrderQuantity> OrderQuantities { get; set; }

        public IList<LumberCategory> LumberCategories { get; set; }

        public IList<MiscellaneousCategory> MiscellaneousCategories { get; set; }
    }
}