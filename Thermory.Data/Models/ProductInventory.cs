using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Data.Models
{
    [Table("ProductInventory")]
    internal class ProductInventory : IDbProductInventory
    {
        [Key]
        public Guid Id { get; set; }

        public int Quantity { get; set; }
    }
}
