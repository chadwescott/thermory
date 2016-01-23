using System;

namespace Thermory.Data.Models
{
    public interface IDbProduct
    {
        Guid Id { get; }
        string Name { get; }
        string Description { get; }
    }
}
