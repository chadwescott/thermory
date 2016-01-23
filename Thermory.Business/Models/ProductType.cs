using System;
using System.Collections.Generic;
using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class ProductType : IProductType
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }

    internal class ProductType<TSC, TP> : ProductType, IProductType<TSC, TP>
        where TSC : IProductSubCategory
        where TP : IProduct<IProductType>
    {
        public TSC SubCategory { get; set; }
        public IList<TP> Products { get; set; }
    }
}
