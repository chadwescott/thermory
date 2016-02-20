using System;
using System.Collections.Generic;

namespace Thermory.Domain
{
    public interface ILumberType
    {
        Guid Id { get; }

        ILumberSubCategory LumberSubCategory { get; }

        string Name { get; }

        int SortOrder { get; }

        int TotalPieces { get; }

        double TotalLinearFeet { get; }

        double TotalSquareFeet { get; }

        int[] LumberLengthsMillimeters { get; }

        double[] LumberLengthsFeet { get; }

        IList<ILumberProduct> LumberProducts { get; }
    }
}
