using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal abstract class ProductInventory : IInventory
    {
        public int Quantity { get; set; }
    }
}
