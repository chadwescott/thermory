using System;

namespace Thermory.Domain
{
    public interface ILumberProduct
    {
        Guid Id { get; }

        int Quantity { get; set; }

        int LengthInMillmeters { get; }

        double LengthInInches { get; }

        double TallyPercentage { get; }

        double LinearFeet { get; }

        double SquareFeet { get; }

        ILumberType LumberType { get; }
    }
}
