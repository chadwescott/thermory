namespace Thermory.Domain
{
    public interface ILumberInventory : IProductInventory<ILumberProductType>
    {
        double LinearFeet { get; set; }
        double SquareFeet { get; set; }
    }
}
