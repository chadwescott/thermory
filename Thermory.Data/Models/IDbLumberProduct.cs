using System;

namespace Thermory.Data.Models
{
    public interface IDbLumberProduct
    {
        Guid Id { get; }

        Guid LumberTypeId { get; }

        IDbLumberType LumberType { get; }

        int Length { get; }

        int Quantity { get; }
    }
}
