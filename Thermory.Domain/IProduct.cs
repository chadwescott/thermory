namespace Thermory.Domain
{
    public interface IProduct<T> where T : IProductSubCategory
    {
        IProductType<T> ProductType { get; }
    }
}
