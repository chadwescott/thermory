using System;

namespace Thermory.Data.Models
{
    internal interface IDbOrder
    {
        Guid Id { get; }

        Guid OrderTypeId { get; }

        IDbOrderType OrderType { get; }
    }
}