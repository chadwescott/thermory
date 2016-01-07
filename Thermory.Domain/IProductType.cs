using System.Collections.Generic;

namespace Thermory.Domain
{
    public interface IProductType<T> where T : IProductSubCategory
    {
        T SubCategory { get; }
        string Name { get; }
        IList<IProduct<T>> Products { get; }
    }
}
