using System;

namespace Thermory.Data.Models
{
    public interface IDbTransactionType
    {
        Guid Id { get; }

        string Name { get; }
    }
}