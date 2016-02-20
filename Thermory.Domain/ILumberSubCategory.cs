using System;
using System.Collections.Generic;

namespace Thermory.Domain
{
    public interface ILumberSubCategory
    {
        Guid Id { get; }

        string Name { get; }

        ILumberCategory LumberCategory { get; }

        int WidthInMillimeters { get; }

        double WidthInInches { get; }

        int ThicknessInMillimeters { get; }

        double ThicknessInInches { get; }

        IList<ILumberType> LumberTypes { get; }
    }
}
