using System;
using System.Collections.Generic;

namespace Thermory.Domain
{
    public interface IProductCategory<T> where T : IProductSubCategory
    {
        Guid Id { get; }
        string Name { get; }

        IList<T> ProductSubCategories { get; }
    }
}
