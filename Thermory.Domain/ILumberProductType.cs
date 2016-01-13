namespace Thermory.Domain
{
    public interface ILumberProductType : IProductType<ILumberSubCategory, ILumberProduct>
    {
        int[] LumberProductLengthsMillimeters { get; }
        double[] LumberProductLengthsFeet { get; }
    }
}
