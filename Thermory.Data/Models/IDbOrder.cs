using System;

namespace Thermory.Data.Models
{
    internal interface IDbOrder
    {
        Guid Id { get; }

        Guid InventoryTransactionId { get; }

        IDbInventoryTransaction InventoryTransaction { get; }

        Guid OrderTypeId { get; }

        IDbOrderType OrderType { get; }
    }
}