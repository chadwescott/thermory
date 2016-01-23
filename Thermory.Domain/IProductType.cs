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

    public interface IProductType<out TSC, TP> : IProductType
        where TSC : IProductSubCategory
        where TP : IProduct<IProductType>
    {
        TSC SubCategory { get; }
        IList<TP> Products { get; }
    }
}
