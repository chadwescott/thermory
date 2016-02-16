using System;

namespace Thermory.Domain
{
    public interface ILumberProduct
    {
        Guid Id { get; }

        ILumberInventory Inventory { get; }

        int LengthInMillmeters { get; }

        double LengthInInches { get; }
    }
}
