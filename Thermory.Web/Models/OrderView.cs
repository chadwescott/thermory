using System;
using System.ComponentModel.DataAnnotations.Schema;
using Thermory.Domain.Constants;
using Thermory.Domain.Models;

namespace Thermory.Web.Models
{
    [Table("Order_V")]
    public class OrderView : IViewModel
    {
        [Column("OrderId")]
        public string recid { get; set; }

        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column("OrderNumber")]
        public int OrderNumber { get; set; }

        [NotMapped]
        public string OrderNumberString { get { return OrderNumber.ToString(Formats.OrderNumberFormat); } }

        [Column("OrderType")]
        public string OrderType { get; set; }

        [Column("CustomerName")]
        public string CustomerName { get; set; }

        [Column("CreatedBy")]
        public string CreatedBy { get; set; }

        [Column("CreatedOn")]
        public DateTime CreatedOn { get; set; }
    }
}