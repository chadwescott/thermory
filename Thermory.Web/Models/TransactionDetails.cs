using System.Collections.Generic;

namespace Thermory.Web.Models
{
    public abstract class TransactionDetails<T>
    {
        public int AdjustmentMultiplier { get; set; }

        public List<T> Details { get; set; }
    }
}