using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class Product<T> : IProduct<T> where T : IProductSubCategory
    {
        public IProductType<T> ProductType { get; set; }
    }
}
