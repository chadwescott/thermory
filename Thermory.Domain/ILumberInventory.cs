using System;

namespace Thermory.Domain
{
    public interface ILumberInventory
    {
        Guid LumberProductId { get; }

        int Quantity { get; set; }

        double TallyPercentage { get; }

        double LinearFeet { get; }

        double SquareFeet { get; }
    }
}
