using System;

namespace Thermory.Data.Models
{
    internal interface IDbOrderType
    {
        Guid Id { get; }

        string Name { get; }
    }
}