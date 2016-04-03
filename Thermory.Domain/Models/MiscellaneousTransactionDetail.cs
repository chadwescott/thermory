using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Domain.Models
{
    public class MiscellaneousTransactionDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid InventoryTransactionId { get; set; }

        public Guid MiscellaneousProductId { get; set; }

        [ForeignKey("MiscellaneousProductId")]
        public MiscellaneousProduct MiscellaneousProduct { get; set; }

        public int PreviousQuantity { get; set; }

        public int NewQuantity { get; set; }
    }
}
