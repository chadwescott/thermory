﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Thermory.Domain.Enums;

namespace Thermory.Domain.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid? CustomerId { get; set; }

        public string OrderNumber { get; set; }

        public Guid? OrderStatusId { get; set; }

        public Guid OrderTypeId { get; set; }

        public Guid? PackagingTypeId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [ForeignKey("OrderStatusId")]
        public OrderStatus OrderStatus { get; set; }

        [NotMapped]
        public int HoursToLoad { get { return MinutesToLoad == null ? 0 : MinutesToLoad.Value / 60; } }

        public int? MinutesToLoad { get; set; }

        [NotMapped]
        public int MinutesToLoadRemainder { get { return MinutesToLoad == null ? 0 : MinutesToLoad.Value % 60; } }

        [NotMapped]
        public int HoursToPull { get { return MinutesToPull == null ? 0 : MinutesToPull.Value / 60; } }

        public int? MinutesToPull { get; set; }

        [NotMapped]
        public int MinutesToPullRemainder { get { return MinutesToPull == null ? 0 : MinutesToPull.Value % 60; } }

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

        [NotMapped]
        public bool ApplyInventoryQuantityChanges
        {
            get
            {
                return OrderType.OrderTypeEnum == OrderTypes.SalesOrder ||
                       OrderStatus.OrderStatusEnum == OrderStatuses.Received;
            }
        }

        [NotMapped]
        public int TotalPieces
        {
            get { return OrderLumberLineItems.Any() ? OrderLumberLineItems.Sum(oli => oli.Quantity) : 0; }
        }

        [NotMapped]
        public double TotalLinearFeet
        {
            get { return OrderLumberLineItems.Any() ? OrderLumberLineItems.Sum(oli => oli.LinearFeet) : 0; }
        }

        [NotMapped]
        public double TotalSquareFeet
        {
            get { return OrderLumberLineItems.Any() ? OrderLumberLineItems.Sum(oli => oli.SquareFeet) : 0; }
        }

        [NotMapped]
        public double PiecesLoadedPerHour
        {
            get { return MinutesToLoad == null || MinutesToLoad == 0 || TotalPieces == 0 ? 0 : TotalPieces / ((double)MinutesToLoad.Value / 60); }
        }

        [NotMapped]
        public double SquareFeetLoadedPerHour
        {
            get { return MinutesToLoad == null || MinutesToLoad == 0 || TotalSquareFeet == 0 ? 0 : TotalSquareFeet / ((double)MinutesToLoad.Value / 60); }
        }

        [NotMapped]
        public double LinearFeetLoadedPerHour
        {
            get { return MinutesToLoad == null || MinutesToLoad == 0 || TotalLinearFeet == 0 ? 0 : TotalLinearFeet / ((double)MinutesToLoad.Value / 60); }
        }

        [NotMapped]
        public double PiecesPulledPerHour
        {
            get {  return MinutesToPull == null || MinutesToPull == 0 || TotalPieces == 0 ? 0 : TotalPieces / ((double)MinutesToPull.Value / 60);}
        }

        [NotMapped]
        public double SquareFeetPulledPerHour
        {
            get { return MinutesToPull == null || MinutesToPull == 0 || TotalSquareFeet == 0 ? 0 : TotalSquareFeet / ((double)MinutesToPull.Value / 60); }
        }

        [NotMapped]
        public double LinearFeetPulledPerHour
        {
            get { return MinutesToPull == null || MinutesToPull == 0 || TotalLinearFeet == 0 ? 0 : TotalLinearFeet / ((double)MinutesToPull.Value / 60); }
        }
    }
}