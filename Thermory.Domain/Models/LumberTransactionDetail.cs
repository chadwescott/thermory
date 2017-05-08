using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Domain.Models
{
    public class LumberTransactionDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid InventoryTransactionId { get; set; }

        [ForeignKey("InventoryTransactionId")]
        public InventoryTransaction InventoryTransaction { get; set; }

        public Guid LumberProductId { get; set; }

        [ForeignKey("LumberProductId")]
        public LumberProduct LumberProduct { get; set; }

        public int PreviousQuantity { get; set; }

        public int NewQuantity { get; set; }

        [NotMapped]
        public int Delta { get { return NewQuantity - PreviousQuantity; } }
    }
}
