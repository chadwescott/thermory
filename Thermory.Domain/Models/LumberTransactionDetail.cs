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

        public Guid LumberProductId { get; set; }

        public int PreviousQuantity { get; set; }

        public int NewQuantity { get; set; }
    }
}
