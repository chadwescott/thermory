using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Data.Models
{
    internal class Order : IDbOrder
    {
        [Key]
        public Guid Id { get; set; }

        public Guid InventoryTransactionId { get; set; }

        [ForeignKey("InventoryTransactionId")]
        public InventoryTransaction DbInventoryTransaction { get; set; }

        [NotMapped]
        public IDbInventoryTransaction InventoryTransaction { get { return DbInventoryTransaction; } }

        public string OrderType { get; set; }
    }
}