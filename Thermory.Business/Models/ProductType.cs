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

    internal class ProductType<TSC, TPT, TP> : ProductType, IProductType<TSC, TPT, TP>
        where TSC : IProductSubCategory
        where TPT : IProductType
        where TP : IProduct<TPT>
    {
        public TSC SubCategory { get; set; }
        public IList<TP> Products { get; set; }
    }
}
