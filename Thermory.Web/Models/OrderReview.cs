using System.Collections.Generic;
using Thermory.Domain.Models;

namespace Thermory.Web.Models
{
    public class OrderReview
    {
        public Order Order { get; set; }

        public IList<InventoryTransaction> InventoryTransactions { get; set; } 
    }
}