using System;

namespace Thermory.Data.Models
{
    public interface IDbProductInventory
    {
        Guid Id { get; }
        int Quantity { get; }
    }
}
