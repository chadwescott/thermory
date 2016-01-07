namespace Thermory.Domain
{
    public interface ILumberSubCategory : IProductSubCategory
    {
        IProductCategory<ILumberSubCategory> Category { get; } 
        int Width { get; }
        int Thickness { get; }
    }
}
