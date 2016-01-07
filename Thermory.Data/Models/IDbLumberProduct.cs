using System;
namespace Thermory.Data.Models
{

    public interface IDbLumberProduct
    {
        Guid Id { get; }

        Guid LumberFamilyId { get; }

        int Length { get; }
    }
}
