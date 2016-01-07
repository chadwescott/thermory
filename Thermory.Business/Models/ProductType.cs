using System.Collections.Generic;
using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class ProductType<T> : IProductType<T> where T : IProductSubCategory
    {
        public T SubCategory { get; set; }
        public string Name { get; set; }
        public IList<IProduct<T>> Products { get; set; }
    }
}
