namespace Thermory.Domain
{
    public interface ILumberProductType
    {
        int TotalPieces { get; }

        double TotalLinearFeet { get; }

        double TotalSquareFeet { get; }

        int[] LumberProductLengthsMillimeters { get; }

        double[] LumberProductLengthsFeet { get; }
    }
}
