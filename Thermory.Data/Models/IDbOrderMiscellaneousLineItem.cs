using System;

namespace Thermory.Data.Models
{
    internal interface IDbOrderMiscellaneousLineItem
    {
        Guid Id { get; }

        Guid OrderId { get; }

        Guid MiscellaneousProductId { get; }

        IDbOrder Order { get; }

        IDbMiscellaneousProduct MiscellaneousProduct { get; }

        int Quantity { get; }
    }
}