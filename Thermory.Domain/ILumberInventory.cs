namespace Thermory.Domain
{
    public interface ILumberInventory : IInventory
    {
        ILumberProduct Product { get; }

        double TallyPercentage { get; }

        double LinearFeet { get; }

        double SquareFeet { get; }
    }
}
