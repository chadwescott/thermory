namespace Thermory.Domain
{
    public interface ILumberProduct : IProduct<ILumberProductType, ILumberInventory, ILumberProduct>
    {
        int LengthInMillmeters { get; }

        double LengthInInches { get; }
    }
}
