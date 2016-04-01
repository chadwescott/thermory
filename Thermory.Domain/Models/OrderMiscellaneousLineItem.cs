using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Domain.Models
{
    public class OrderMiscellaneousLineItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        public Guid MiscellaneousProductId { get; set; }

        [ForeignKey("MiscellaneousProductId")]
        public MiscellaneousProduct MiscellaneousProduct { get; set; }

        public int Quantity { get; set; }
    }
}
