using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Thermory.Domain.Constants;

namespace Thermory.Domain.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderNumber { get; set; }

        public Guid OrderTypeId { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("OrderTypeId")]
        public OrderType OrderType { get; set; }

        [ForeignKey("OrderId")]
        public List<OrderLumberLineItem> OrderLumberLineItems { get; set; }

        [ForeignKey("OrderId")]
        public List<OrderMiscellaneousLineItem> OrderMiscellaneousLineItems { get; set; }

        [NotMapped]
        public string OrderNumberString { get { return OrderNumber.ToString(Formats.OrderNumberFormat); } }
    }
}