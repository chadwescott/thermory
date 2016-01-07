using System.Collections.Generic;

namespace Thermory.Domain
{
    public interface IProductSubCategory
    {
        string Name { get; }
    }

    public interface IProductSubCategory<T> where T : IProductSubCategory
    {
        IList<IProductType<T>> ProductTypes { get; }
    }
}
