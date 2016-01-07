using System.Collections.Generic;
using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class ProductCategory<T> : IProductCategory<T> where T : IProductSubCategory
    {
        public string Name { get; set; }
        public IList<T> ProductSubCategories { get; set; }
    }
}
