namespace Thermory.Domain
{
    public interface IInventory<T> where T : IProduct
    {
        T Product { get; }

        int Quantity { get; set; }
    }
}
