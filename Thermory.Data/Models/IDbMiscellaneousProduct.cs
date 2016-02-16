using System;

namespace Thermory.Data.Models
{
    public interface IDbMiscellaneousProduct
    {
        Guid Id { get; }

        string Name { get; }

        string Description { get; }

        int Quantity { get; }
    }
}
