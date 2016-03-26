﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using Thermory.Domain.Constants;

namespace Thermory.Web.Models
{
    [Table("Order_V")]
    public class OrderView : IViewModel
    {
        [Column("OrderId")]
        public string recid { get; set; }

        [Column("OrderNumber")]
        public int OrderNumber { get; set; }

        [NotMapped]
        public string OrderNumberString { get { return OrderNumber.ToString(Formats.OrderNumberFormat); } }

        [Column("OrderType")]
        public string OrderType { get; set; }

        [Column("CreatedBy")]
        public string CreatedBy { get; set; }

        [Column("CreatedOn")]
        public DateTime CreatedOn { get; set; }
    }
}