using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class Product<T> : IProduct<T> where T : IProductType
    {
        public T ProductType { get; set; }

        public int Quantity { get; set; }
    }
}
