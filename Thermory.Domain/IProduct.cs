namespace Thermory.Domain
{
    public interface IProduct<out T> where T : IProductType
    {
        T ProductType { get; }
    }
}
