using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class ProductInventory<T> : IProductInventory<T> where T : IProductType
    {
        public IProduct<T> Product { get; set; }
        public int Quantity { get; set; }
    }
}
