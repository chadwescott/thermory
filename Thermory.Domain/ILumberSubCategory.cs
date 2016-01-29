using System.Collections.Generic;

namespace Thermory.Domain
{
    public interface ILumberSubCategory : IProductSubCategory
    {
        IProductCategory<ILumberSubCategory> Category { get; }
        int WidthInMillimeters { get; }
        double WidthInInches { get; }
        int ThicknessInMillimeters { get; }
        double ThicknessInInches { get; }
        IList<ILumberProductType> ProductTypes { get; }
    }
}
