namespace Thermory.Domain
{
    public interface ILumberProductType //: IProductType<ILumberSubCategory, ILumberProductType, ILumberInventory, ILumberProduct>
    {
        int TotalPieces { get; }

        double TotalLinearFeet { get; }

        double TotalSquareFeet { get; }

        int[] LumberProductLengthsMillimeters { get; }

        double[] LumberProductLengthsFeet { get; }
    }
}
