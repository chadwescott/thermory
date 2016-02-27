using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Data.Models
{
    internal class OrderMiscellaneousLineItem : IDbOrderMiscellaneousLineItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order DbOrder { get; set; }

        public Guid MiscellaneousProductId { get; set; }

        public IDbOrder Order { get { return DbOrder; } }

        [ForeignKey("MiscellaneousProductId")]
        public MiscellaneousProduct DbMiscellaneousProduct { get; set; }

        public IDbMiscellaneousProduct MiscellaneousProduct { get { return DbMiscellaneousProduct; } }

        public int Quantity { get; set; }
    }
}
