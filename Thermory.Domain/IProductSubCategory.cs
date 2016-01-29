using System;
using System.Collections.Generic;

namespace Thermory.Domain
{
    public interface IProductSubCategory
    {
        Guid Id { get; }

        string Name { get; }
    }

    public interface IProductSubCategory<TPT, TP>
        where TPT : IProductType
        where TP : IProduct<TPT>
    {
        IList<IProductType<IProductSubCategory, TPT, TP>> ProductTypes { get; }
    }
}
