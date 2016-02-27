using System;

namespace Thermory.Data.Models
{
    internal interface IDbOrderLumberLineItem
    {
        Guid Id { get; }

        Guid OrderId { get; }

        Guid LumberProductId { get; }

        IDbOrder Order { get; }

        IDbLumberProduct LumberProduct { get; }

        int Quantity { get; }
    }
}