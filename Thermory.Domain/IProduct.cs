using System;

namespace Thermory.Domain
{
    public interface IProduct
    {
        Guid Id { get; }
    }

    public interface IProduct<out T> : IProduct
        where T : IProductType
    {
        T ProductType { get; }
    }
}
