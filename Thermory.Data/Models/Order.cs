using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Data.Models
{
    internal class Order : IDbOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid InventoryTransactionId { get; set; }

        [ForeignKey("InventoryTransactionId")]
        public InventoryTransaction DbInventoryTransaction { get; set; }

        [NotMapped]
        public IDbInventoryTransaction InventoryTransaction { get { return DbInventoryTransaction; } }

        public Guid OrderTypeId { get; set; }

        [ForeignKey("OrderTypeId")]
        public OrderType DbOrderType { get; set; }

        [NotMapped]
        public IDbOrderType OrderType { get { return DbOrderType; } }
    }
}