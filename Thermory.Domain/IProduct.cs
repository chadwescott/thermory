using System;

namespace Thermory.Domain
{
    public interface IProduct
    {
        Guid Id { get; }
    }

    public interface IProduct<out TT, TI, TP> : IProduct
        where TT : IProductType
        where TI : IInventory<TP>
        where TP : IProduct<TT, TI, TP>
    {
        TT ProductType { get; }

        TI Inventory { get; set; }
    }
}
