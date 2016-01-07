using System.Collections.Generic;

namespace Thermory.Domain
{
    public interface IProductCategory<T> where T : IProductSubCategory
    {
        string Name { get; }

        IList<T> ProductSubCategories { get; }
    }
}
