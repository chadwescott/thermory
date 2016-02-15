using System;

namespace Thermory.Data.Models
{
    public interface IDbProduct
    {
        Guid Id { get; }

        Guid ProductFamilyId { get; set; }

        IDbProductInventory Inventory { get; }
    }
}
