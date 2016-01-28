using System;
namespace Thermory.Data.Models
{

    public interface IDbLumberProduct : IDbProduct
    {
        Guid LumberFamilyId { get; }

        int Length { get; }
    }
}
