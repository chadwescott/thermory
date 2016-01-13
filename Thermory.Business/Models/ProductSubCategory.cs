using System;
using System.Collections.Generic;
using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class ProductSubCategory : IProductSubCategory
    {
        public Guid Id { get; set; }
        public IProductCategory<ProductSubCategory> Category { get; set; }
        public string Name { get; set; }
        public IList<IProductType> ProductTypes { get; set; }
    }
}
