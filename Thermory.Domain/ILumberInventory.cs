namespace Thermory.Domain
{
    public interface ILumberInventory
    {
        ILumberProduct Lumber { get; }

        int Quantity { get; set; }

        double TallyPercentage { get; }

        double LinearFeet { get; }

        double SquareFeet { get; }
    }
}
