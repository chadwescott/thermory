namespace Thermory.Domain
{
    public interface IProductInventory<T> where T : IProductType
    {
        IProduct<T> Product { get; }
        int Quantity { get; }
    }
}
