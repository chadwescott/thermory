using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Domain.Models
{
    public class OrderLumberLineItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        public Guid OrderStatusId { get; set; }

        [ForeignKey("OrderStatusId")]
        public OrderStatus OrderStatus { get; set; }

        public Guid LumberProductId { get; set; }

        [ForeignKey("LumberProductId")]
        public LumberProduct LumberProduct { get; set; }

        public int Quantity { get; set; }

        [NotMapped]
        public double LinearFeet { get { return LumberProduct.LengthInInches * Quantity; } }

        [NotMapped]
        public double SquareFeet { get { return Math.Round(LinearFeet * LumberProduct.LumberType.LumberSubCategory.WidthInInches / 12, 0); } }
    }
}
