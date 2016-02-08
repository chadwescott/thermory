using System;
using System.Collections.Generic;

namespace Thermory.Domain
{
    public interface IProductType
    {
        Guid Id { get; }
        string Name { get; }
        string Description { get; }
    }

    public interface IProductType<out TSC, TPT, TI, TP> : IProductType
        where TSC : IProductSubCategory
        where TPT : IProductType
        where TI : IInventory<TP>
        where TP : IProduct<TPT, TI, TP>
    {
        TSC SubCategory { get; }
        IList<TP> Products { get; }
    }
}
