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

    internal class ProductType<TSC, TPT, TI, TP> : ProductType, IProductType<TSC, TPT, TI, TP>
        where TSC : IProductSubCategory
        where TPT : IProductType
        where TI : IInventory<TP>
        where TP : IProduct<TPT, TI, TP>
    {
        public TSC SubCategory { get; set; }
        public IList<TP> Products { get; set; }
    }
}
