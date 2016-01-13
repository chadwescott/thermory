namespace Thermory.Domain
{
    public interface ILumberProduct : IProduct<ILumberProductType>
    {
        int LengthInMillmeters { get; }
        double LengthInInches { get; }
    }
}
