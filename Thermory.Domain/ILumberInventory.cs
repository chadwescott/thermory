namespace Thermory.Domain
{
    public interface ILumberInventory : IInventory<ILumberProduct>
    {
        double TallyPercentage { get; }

        double LinearFeet { get; }

        double SquareFeet { get; }
    }
}
