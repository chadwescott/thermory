using System.Collections.Generic;
using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class ProductSubCategory : IProductSubCategory
    {
        public IProductCategory<ProductSubCategory> Category { get; set; }
        public string Name { get; set; }
        public IList<IProductType<IProductSubCategory>> ProductTypes { get; set; }
    }
}
