using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Data.Models
{
    internal class OrderLumberLineItem : IDbOrderLumberLineItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order DbOrder { get; set; }

        public Guid LumberProductId { get; set; }

        public IDbOrder Order { get { return DbOrder; } }

        [ForeignKey("LumberProductId")]
        public LumberProduct DbLumberProduct { get; set; }

        public IDbLumberProduct LumberProduct { get { return DbLumberProduct; } }

        public int Quantity { get; set; }
    }
}
