using System;

namespace Thermory.Data.Models
{
    public interface IDbMiscellaneousTransactionDetail
    {
        Guid Id { get; set; }

        Guid InventoryTransactionId { get; set; }

        Guid MiscellaneousProductId { get; set; }

        int PreviousQuantity { get; set; }

        int NewQuantity { get; set; }
    }
}