using System;
using System.Collections.Generic;

namespace Thermory.Domain
{
    public interface IProductSubCategory
    {
        Guid Id { get; }

        string Name { get; }
    }

    public interface IProductSubCategory<TPT, TI, TP>
        where TPT : IProductType
        where TI : IInventory<TP>
        where TP : IProduct<TPT, TI, TP>
    {
        IList<IProductType<IProductSubCategory, TPT, TI, TP>> ProductTypes { get; }
    }
}
