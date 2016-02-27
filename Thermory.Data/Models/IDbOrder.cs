using System;

namespace Thermory.Data.Models
{
    internal interface IDbOrder
    {
        Guid Id { get; set; }

        Guid InventoryTransactionId { get; set; }

        IDbInventoryTransaction InventoryTransaction { get; }

        string OrderType { get; set; }
    }
}