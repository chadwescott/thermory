using System;
using System.Collections.Generic;

namespace Thermory.Domain
{
    public interface IProductSubCategory
    {
        Guid Id { get; }
        string Name { get; }
    }

    public interface IProductSubCategory<TP>
        where TP : IProduct<IProductType>
    {
        IList<IProductType<IProductSubCategory, TP>> ProductTypes { get; }
    }
}
