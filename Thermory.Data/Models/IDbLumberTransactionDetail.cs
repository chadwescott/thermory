using System;

namespace Thermory.Data.Models
{
    public interface IDbLumberTransactionDetail
    {
        Guid Id { get; set; }

        Guid InventoryTransactionId { get; set; }

        Guid LumberProductId { get; set; }

        int PreviousQuantity { get; set; }

        int NewQuantity { get; set; }
    }
}