using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal abstract class ProductInventory<T> : IInventory<T>
        where T : IProduct
    {
        public T Product { get; set; }

        public int Quantity { get; set; }
    }
}
