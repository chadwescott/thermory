using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Thermory.Domain.Enums;

namespace Thermory.Domain.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid? CustomerId { get; set; }

        [Required]
        public string OrderNumber { get; set; }

        public Guid? OrderStatusId { get; set; }

        public Guid OrderTypeId { get; set; }

        public Guid? PackagingTypeId { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [ForeignKey("OrderStatusId")]
        public OrderStatus OrderStatus { get; set; }

        [ForeignKey("OrderTypeId")]
        public OrderType OrderType { get; set; }

        [ForeignKey("PackagingTypeId")]
        public PackagingType PackagingType { get; set; }

        [ForeignKey("OrderId")]
        public List<OrderLumberLineItem> OrderLumberLineItems { get; set; }

        [ForeignKey("OrderId")]
        public List<OrderMiscellaneousLineItem> OrderMiscellaneousLineItems { get; set; }

        [ForeignKey("OrderId")]
        public List<InventoryTransaction> InventoryTransactions { get; set; }

        [NotMapped]
        public int AdjustmentMultiplier { get { return OrderType.OrderTypeEnum == OrderTypes.PurchaseOrder ? 1 : -1; } }
    }
}