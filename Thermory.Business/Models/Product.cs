using System;
using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class Product<TT, TI, TP> : IProduct<TT, TI, TP>
        where TT : IProductType
        where TI : IInventory<TP>
        where TP : IProduct<TT, TI, TP>
    {
        public Guid Id { get; set; }

        public TT ProductType { get; set; }
        
        public TI Inventory { get; set; }
    }
}
